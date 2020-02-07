//  Copyright 2020 Jonguk Kim
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using Newtonsoft.Json;
using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace MNY2
{
    public partial class AnalyzeDialog : RadForm
    {
        public AnalyzeDialog()
        {
            InitializeComponent();
        }


        private void GetMapInfo()
        {
            try
            {
                DialogResult res = DialogResult.No;

                Invoke(new Action(() => { res = new VersionSelectDialog().ShowDialog(this); }));

                if (res != DialogResult.OK)
                {
                    DialogResult = DialogResult.Abort;
                    Close();
                    return;
                }

                var locker = new object();
                var misc = new MiscInfo();
                var mapInfos = new ConcurrentBag<MapInfo>();
                var maps = Directory.GetFiles(Program.ClientPath, "*.wz")
                    .Where(a => Path.GetFileNameWithoutExtension(a).ToLower().Contains("map"))
                    .Select(a =>
                        new WZFile(a, Program.WzVariant, Program.IsEncrypted, WZReadSelection.EagerParseStrings))
                    .ToList();
                var mapIndicators = new int[4]; //순서: Map, Back, Obj, Tile
                CheckForIllegalCrossThreadCalls = false;
                if (maps.Count == 0)
                {
                    MessageBox.Show(Strings.NoMapInfo, Strings.Error);
                    DialogResult = DialogResult.Abort;
                    Close();
                    Environment.Exit(2);
                }

                for (var i = 0; i < maps.Count; i++)
                {
                    if (maps[i].MainDirectory.HasChild("Map")) mapIndicators[0] = i;
                    if (maps[i].MainDirectory.HasChild("Back")) mapIndicators[1] = i;
                    if (maps[i].MainDirectory.HasChild("Obj")) mapIndicators[2] = i;
                    if (maps[i].MainDirectory.HasChild("Tile")) mapIndicators[3] = i;
                }

                var mapDir = maps[mapIndicators[0]].MainDirectory["Map"];
                var backDir = maps[mapIndicators[1]].MainDirectory["Back"];
                var objDir = maps[mapIndicators[2]].MainDirectory["Obj"];
                var tileDir = maps[mapIndicators[3]].MainDirectory["Tile"];
                if (IsHandleCreated)
                    Invoke(new Action(() =>
                        radProgressBar1.Maximum =
                            mapDir.Where(obj => obj is WZDirectory).Sum(dir => dir.ChildCount + 3)));

                foreach (var dir in mapDir.Where(o => o is WZDirectory))
                {
                    Parallel.ForEach(dir, new ParallelOptions { MaxDegreeOfParallelism = 4 }, img =>
                      {
                          lock (locker)
                          {
                              var info = new MapInfo { Id = int.Parse(img.Name.Remove(9)), Size = (img as WZImage).DumpBytes().Length };
                              if (img.HasChild("info"))
                              {
                                  if (img["info"].HasChild("bgm"))
                                  {
                                      var bgm = img.ResolvePath("info/bgm").ValueOrDie<string>();
                                      info.BGM = bgm.Length == 0 ? null : bgm;
                                  }
                              }

                              if (img.HasChild("life"))
                              {
                                  if (img["life"].ChildCount > 0)
                                  {
                                      if (img["life"].HasChild("isCategory"))
                                      {
                                          var filteredLives = img["life"].Where(o =>
                                                  o.ChildCount > 0 && o.HasChild("type") && o.HasChild("id"))
                                              .SelectMany(o => o).Where(o => o.HasChild("id") && o.HasChild("type")).ToArray();
                                          var mobs = filteredLives.Where(o => o["type"].ValueOrDie<string>() == "m")
                                              .Select(o => o["id"].ValueOrDie<string>()).Distinct().Select(int.Parse)
                                              .OrderBy(s => s).ToList();
                                          var npcs = filteredLives.Where(o => o["type"].ValueOrDie<string>() == "n")
                                              .Select(o => o["id"].ValueOrDie<string>()).Distinct().Select(int.Parse)
                                              .OrderBy(s => s).ToList();
                                          info.Monsters = mobs.Count == 0 ? null : mobs;
                                          info.NPCs = npcs.Count == 0 ? null : npcs;
                                      }
                                      else
                                      {
                                          var filteredLives = img.ResolvePath("life").Where(o =>
                                              o.ChildCount > 0 && o.HasChild("type") && o.HasChild("id")).ToArray();
                                          var mobs = filteredLives.Where(o =>
                                                  o.ResolvePath("type").ValueOrDie<string>() == "m")
                                              .Select(o => o.ResolvePath("id").ValueOrDie<string>()).Distinct()
                                              .Select(int.Parse)
                                              .OrderBy(id => id).ToList();
                                          var npcs = filteredLives.Where(o =>
                                                  o.ResolvePath("type").ValueOrDie<string>() == "n")
                                              .Select(o => o.ResolvePath("id").ValueOrDie<string>()).Distinct()
                                              .Select(int.Parse)
                                              .OrderBy(id => id).ToList();
                                          info.Monsters = mobs.Count == 0 ? null : mobs;
                                          info.NPCs = npcs.Count == 0 ? null : npcs;
                                      }
                                  }
                              }

                              if (img.HasChild("back"))
                              {
                                  if (img["back"].ChildCount > 0)
                                  {
                                      var backs = img.ResolvePath("back")
                                          .Select(o => o.ResolvePath("bS").ValueOrDie<string>()).Where(s => s.Length != 0)
                                          .Distinct().OrderBy(name => name).ToList();
                                      info.Backgrounds = backs.Count == 0 ? null : backs;
                                  }
                              }

                              var objs = Enumerable.Range(0, 8).Where(n => img.HasChild(n.ToString()))
                                  .Select(n => img.ResolvePath($"{n}/obj"))
                                  .Where(o => o.ChildCount > 0)
                                  .Select(o =>
                                      Enumerable.Range(0, o.ChildCount)
                                          .Select(n => o.ResolvePath($"{n}/oS").ValueOrDie<string>()))
                                  .SelectMany(a => a).Distinct().OrderBy(s => s).ToList();
                              var tiles = Enumerable.Range(0, 8).Where(n => img.HasChild(n.ToString()))
                                  .Select(n => img.ResolvePath($"{n}/info"))
                                  .Where(o => o.HasChild("tS"))
                                  .Select(o => o.ResolvePath("tS").ValueOrDie<string>())
                                  .Distinct().OrderBy(s => s).ToList();

                              info.Objects = objs.Count == 0 ? null : objs;
                              info.Tiles = tiles.Count == 0 ? null : tiles;

                              if (img.HasChild("reactor"))
                              {
                                  if (img["reactor"].ChildCount > 0)
                                  {
                                      var reactors = img["reactor"].Select(o => o.ResolvePath("id").ValueOrDie<string>())
                                          .Distinct().ToList();
                                      info.Reactors = reactors.Count == 0 ? null : reactors;
                                  }
                              }

                              mapInfos.Add(info);
                          }

                          StepProgressBar();
                      });
                }

                misc.Backgrounds = backDir.Where(o => o is WZImage).Cast<WZImage>().OrderBy(o => o.Name)
                      .ToDictionary(a => a.Name.Replace(".img", ""), a => a.DumpBytes().Length);
                StepProgressBar();
                misc.Objects = objDir.Where(o => o is WZImage).Cast<WZImage>().OrderBy(o => o.Name)
                      .ToDictionary(a => a.Name.Replace(".img", ""), a => a.DumpBytes().Length);
                StepProgressBar();
                misc.Tiles = tileDir.Where(o => o is WZImage).Cast<WZImage>().OrderBy(o => o.Name)
                      .ToDictionary(a => a.Name.Replace(".img", ""), a => a.DumpBytes().Length);
                StepProgressBar();

                foreach (var map in maps)
                {
                    map.Dispose();
                }

                maps.Clear();

                File.WriteAllText("MapInfo.dat", JsonConvert.SerializeObject(
                    mapInfos.OrderBy(item => item.Id).ToList(),
                    Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }));

                var loProcess = Process.GetCurrentProcess();
                loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
                loProcess.MinWorkingSet = loProcess.MinWorkingSet;

                if (IsHandleCreated)
                    Invoke(new Action(() =>
                    {
                        radProgressBar1.Maximum = 8;
                        radProgressBar1.Value1 = 0;
                        radProgressBar1.Text = "0%";
                        radLabel1.Text = Strings.Analyzing2;
                    }));

                GetSoundInfo(misc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Strings.Error);
                Environment.Exit(999);
            }
        }

        private void GetSoundInfo(MiscInfo info)
        {
            var sounds = Directory.GetFiles(Program.ClientPath, "*.wz")
                .Where(a => Path.GetFileNameWithoutExtension(a).ToLower().Contains("sound") && !a.Contains("001"))
                .Select(a => new WZFile(a, Program.WzVariant, Program.IsEncrypted, WZReadSelection.EagerParseImage)).ToList();
            if (sounds.Count == 0)
            {
                MessageBox.Show(Strings.NoSoundInfo, Strings.Error);
                DialogResult = DialogResult.Abort;
                Close();
                Environment.Exit(2);
            }

            info.Sounds = sounds.Select(f => f.MainDirectory).SelectMany(a => a)
                .SelectMany(a => a, (a, b) => new { a, b })
                .Where(t => (t.a.Path.ToLower().Contains("bgm") || t.a.Path.Contains("PL_")) && !t.a.Path.Contains("UI") && !t.a.Path.Contains("PL_Sound")).Select(o => o.b)
                .Where(o => o is WZAudioProperty)
                .Cast<WZAudioProperty>().ToDictionary(a => a.Path.Remove(0, 1).Replace(".img", ""), a => a.Value.Length + (a.Header?.Length ?? 0))
                .OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);

            foreach (var f in sounds) f.Dispose();
            sounds.Clear();
            StepProgressBar();
            var loProcess = Process.GetCurrentProcess();
            loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
            loProcess.MinWorkingSet = loProcess.MinWorkingSet;
            GetNpcInfo(info);
        }

        private void GetNpcInfo(MiscInfo info)
        {
            var npc = Directory.GetFiles(Program.ClientPath, "*.wz")
                .Where(a => Path.GetFileNameWithoutExtension(a).ToLower().Contains("npc"))
                .Select(a => new WZFile(a, Program.WzVariant, Program.IsEncrypted, WZReadSelection.NeverParseCanvas)).ToList();

            if (npc.Count == 0)
            {
                MessageBox.Show(Strings.NoNpcInfo, Strings.Error);
                DialogResult = DialogResult.Abort;
                Close();
                Environment.Exit(2);
            }

            info.Npcs = npc.Select(f => f.MainDirectory).SelectMany(o => o)
                .Where(o => o is WZImage)
                .Cast<WZImage>().ToDictionary(a => int.Parse(a.Name.Replace(".img", "")), a => a.DumpBytes().Length)
                .OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);

            StepProgressBar();
            var loProcess = Process.GetCurrentProcess();
            loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
            loProcess.MinWorkingSet = loProcess.MinWorkingSet;
            GetMobInfo(info);
        }

        private void GetMobInfo(MiscInfo info)
        {
            var mob = Directory.GetFiles(Program.ClientPath, "*.wz")
                .Where(a => Path.GetFileNameWithoutExtension(a).ToLower().Contains("mob") && !Path.GetFileNameWithoutExtension(a).ToLower().Contains("taming"))
                .Select(a => new WZFile(a, Program.WzVariant, Program.IsEncrypted, WZReadSelection.NeverParseCanvas)).ToList();

            if (mob.Count == 0)
            {
                MessageBox.Show(Strings.NoMobInfo, Strings.Error);
                DialogResult = DialogResult.Abort;
                Close();
                Environment.Exit(2);
            }

            var questCountGroup = mob.Where(f => f.MainDirectory.HasChild("QuestCountGroup"))
                .SelectMany(o => o.MainDirectory["QuestCountGroup"]).Where(o => o is WZImage).Cast<WZImage>()
                .ToDictionary(a => int.Parse(a.Name.Replace(".img", "")), a => a.DumpBytes().Length);
            var mobs = mob.Select(f => f.MainDirectory).SelectMany(o => o)
                .Where(o => o is WZImage).Cast<WZImage>()
                .ToDictionary(a => int.Parse(a.Name.Replace(".img", "")), a => a.DumpBytes().Length);
            info.Monsters = questCountGroup.Count == 0
                ? mobs.OrderBy(o => o.Key).ToDictionary(o => o.Key, o => o.Value)
                : mobs.Union(questCountGroup).OrderBy(o => o.Key)
                    .GroupBy(g => g.Key)
                    .ToDictionary(a => a.Key, a => a.First().Value);
            StepProgressBar();
            var loProcess = Process.GetCurrentProcess();
            loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
            loProcess.MinWorkingSet = loProcess.MinWorkingSet;
            GetReactorInfo(info);
        }

        private void GetReactorInfo(MiscInfo info)
        {
            if (!File.Exists($"{Program.ClientPath}\\Reactor.wz"))
            {
                MessageBox.Show(Strings.NoReactorInfo, Strings.Error);
                DialogResult = DialogResult.Abort;
                Close();
                Environment.Exit(2);
            }

            using (var reactor = new WZFile($"{Program.ClientPath}\\Reactor.wz", Program.WzVariant, Program.IsEncrypted, WZReadSelection.EagerParseImage))
            {
                info.Reactors = reactor.MainDirectory.Where(o => o is WZImage).Cast<WZImage>().OrderBy(o => o.Name)
                    .ToDictionary(a => a.Name.Replace(".img", ""), a => a.DumpBytes().Length);
            }

            StepProgressBar();
            var loProcess = Process.GetCurrentProcess();
            loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
            loProcess.MinWorkingSet = loProcess.MinWorkingSet;
            GetStringInfo(info);
        }

        private void GetStringInfo(MiscInfo info)
        {
            if (!File.Exists($"{Program.ClientPath}\\String.wz"))
            {
                MessageBox.Show(Strings.NoReactorInfo, Strings.Error);
                DialogResult = DialogResult.Abort;
                Close();
                Environment.Exit(2);
            }

            using (var stringFile = new WZFile($"{Program.ClientPath}\\String.wz", Program.WzVariant, Program.IsEncrypted, WZReadSelection.EagerParseStrings))
            {
                info.MapNames = stringFile.MainDirectory["Map.img"].SelectMany(o => o)
                     .GroupBy(g => int.Parse(g.Name))
                     .ToDictionary(a => a.Key, a => a.First().HasChild("mapName")
                         ? a.First()["mapName"].ValueOrDie<string>().Length == 0 || string.IsNullOrWhiteSpace(a.First()["mapName"].ValueOrDie<string>()) ? Strings.NoName :
                         a.First()["mapName"].ValueOrDie<string>()
                         : Strings.NoName)
                     .OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);
                StepProgressBar();

                info.MobNames = stringFile.MainDirectory["Mob.img"].Select(o => o)
                .ToDictionary(o => int.Parse(o.Name), o => o.HasChild("name") ? o["name"].ValueOrDie<string>().Length == 0 || string.IsNullOrWhiteSpace(o["name"].ValueOrDie<string>()) ? Strings.NoName : o["name"].ValueOrDie<string>() : Strings.NoName)
                .OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);
                StepProgressBar();

                info.NpcNames = stringFile.MainDirectory["Npc.img"].Select(o => o)
                    .ToDictionary(o => int.Parse(o.Name), o => o.HasChild("name") ? o["name"].ValueOrDie<string>().Length == 0 || string.IsNullOrWhiteSpace(o["name"].ValueOrDie<string>()) ? Strings.NoName : o["name"].ValueOrDie<string>() : Strings.NoName)
                    .OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);
                StepProgressBar();
            }

            File.WriteAllText("MiscInfo.dat", JsonConvert.SerializeObject(info, Formatting.Indented));
            StepProgressBar();
            var loProcess = Process.GetCurrentProcess();
            loProcess.MaxWorkingSet = loProcess.MaxWorkingSet;
            loProcess.MinWorkingSet = loProcess.MinWorkingSet;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AnalyzeDialog_Shown(object sender, EventArgs e) => new Thread(GetMapInfo).Start();

        private void StepProgressBar()
        {
            if (IsHandleCreated)
                Invoke(new Action(() =>
                {
                    radProgressBar1.Value1++;
                    radProgressBar1.Text =
                        $"{Math.Round((double)(radProgressBar1.Value1 * 100) / radProgressBar1.Maximum)}%";
                }));
        }
    }
}
