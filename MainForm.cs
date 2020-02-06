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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;

namespace MNY2
{
    public partial class MainForm : RadForm
    {
        private readonly string[] Category = { "Obj", "Back", "Tile", "Mob", "Npc", "Reactor", "BGM" };
        private readonly string[] OrderText = { Strings.OrderName, Strings.OrderSize };
        private Dictionary<string, MapInfo> _infos = new Dictionary<string, MapInfo>();
        private Dictionary<string, int> objBack = new Dictionary<string, int>();
        private Dictionary<string, int> backBack = new Dictionary<string, int>();
        private Dictionary<string, int> tileBack = new Dictionary<string, int>();
        private Dictionary<string, int> reactorBack = new Dictionary<string, int>();
        private Dictionary<string, int> mobBack = new Dictionary<string, int>();
        private Dictionary<string, int> npcBack = new Dictionary<string, int>();
        private Dictionary<string, int> bgmBack = new Dictionary<string, int>();
        private MiscInfo miscInfo;

        public MainForm()
        {
            InitializeComponent();
        }

        private void openJson_Click(object sender, EventArgs e)
        {
            _infos.Clear();
            mobBack.Clear();
            npcBack.Clear();

            var list = JsonConvert.DeserializeObject<List<MapInfo>>(File.ReadAllText("MapInfo.dat"));
            foreach (var item in list) _infos.Add($"{item.Id:D9}", item);
            miscInfo = JsonConvert.DeserializeObject<MiscInfo>(File.ReadAllText("MiscInfo.dat"));

            foreach (var a in miscInfo.Objects)
                objBack.Add($"{a.Key} ({a.Value:n0} {Strings.Bytes})", a.Value);
            foreach (var a in miscInfo.Backgrounds)
                backBack.Add($"{a.Key} ({a.Value:n0} {Strings.Bytes})", a.Value);
            foreach (var a in miscInfo.Tiles)
                tileBack.Add($"{a.Key} ({a.Value:n0} {Strings.Bytes})", a.Value);
            foreach (var a in miscInfo.Reactors)
                reactorBack.Add($"{a.Key} ({a.Value:n0} {Strings.Bytes})", a.Value);
            foreach (var a in miscInfo.Sounds)
                bgmBack.Add($"{a.Key} ({a.Value:n0} {Strings.Bytes})", a.Value);

            foreach (var a in miscInfo.Monsters)
            {
                var name = miscInfo.MobNames.ContainsKey(a.Key) ? miscInfo.MobNames[a.Key] : Strings.NoName;
                mobBack.Add($"{a.Key} - {name} ({a.Value:n0} {Strings.Bytes})", a.Value);
            }
            foreach (var a in miscInfo.Npcs)
            {
                var name = miscInfo.NpcNames.ContainsKey(a.Key) ? miscInfo.NpcNames[a.Key] : Strings.NoName;
                npcBack.Add($"{a.Key} - {name} ({a.Value:n0} {Strings.Bytes})", a.Value);
            }

            foreach (var a in this.Category) this.radDropDownList1.Items.Add(new RadListDataItem(a));
            foreach (var a in this.OrderText) this.radDropDownList2.Items.Add(a);
            this.radDropDownList2.SelectedIndex = 0;
        }

        private void analyzeMap_Click(object sender, EventArgs e)
        {
            if (new AnalyzeDialog().ShowDialog() == DialogResult.OK)
            {
                openJson_Click(sender, e);
                analyzeMap.Enabled = false;
            }
        }

        private void List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var list = (RadListControl)sender;

            if (list.SelectedItem != null)
            {
                var text = list.SelectedItem.Text.Contains('-')
                    ? list.SelectedItem.Text.Remove(list.SelectedItem.Text.IndexOf('-') - 1)
                    : list.SelectedItem.Text.Remove(list.SelectedItem.Text.IndexOf('(') - 1);
                Clipboard.SetText(text);
                MessageBox.Show(Strings.Copied, Strings.Inform);
            }
        }

        private void mapList_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            if (e.Position < 0) return;
            var searchQuery = radListControl1.Items.ToArray()[e.Position].Text;
            searchQuery = searchQuery.Contains('-') ? searchQuery.Remove(searchQuery.IndexOf('-') - 1) : searchQuery.Remove(searchQuery.IndexOf('(') - 1);
            radListControl2.Items.Clear();
            foreach (var item in _infos.Where(item => item.Value.ContainsItem(searchQuery)))
            {
                var name = miscInfo.MapNames.ContainsKey(int.Parse(item.Key)) ? miscInfo.MapNames[int.Parse(item.Key)] : Strings.NoName;
                radListControl2.Items.Add($"{item.Key} - {name} ({item.Value.Size:n0} {Strings.Bytes})");
            }
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            if (miscInfo == null || _infos.Count == 0) return;

            radListControl1.Items.Clear();

            switch (radDropDownList1.Text)
            {
                case "Obj":
                    radListControl1.Items.AddRange(objBack.Keys);
                    break;
                case "Reactor":
                    radListControl1.Items.AddRange(reactorBack.Keys);
                    break;
                case "Back":
                    radListControl1.Items.AddRange(backBack.Keys);
                    break;
                case "Tile":
                    radListControl1.Items.AddRange(tileBack.Keys);
                    break;
                case "Mob":
                    radListControl1.Items.AddRange(mobBack.Keys);
                    break;
                case "Npc":
                    radListControl1.Items.AddRange(npcBack.Keys);
                    break;
                case "BGM":
                    radListControl1.Items.AddRange(bgmBack.Keys);
                    break;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (miscInfo == null || _infos.Count == 0) return;

            radListControl2.Items.Clear();
            var locker = new object();
            var locker2 = new object();
            var items = new List<string>();
            var removeList = new ConcurrentBag<string>();
            switch (radDropDownList1.Text)
            {
                case "Obj":
                    items.AddRange(objBack.Keys);
                    break;
                case "Reactor":
                    items.AddRange(reactorBack.Keys);
                    break;
                case "Back":
                    items.AddRange(backBack.Keys);
                    break;
                case "Tile":
                    items.AddRange(tileBack.Keys);
                    break;
                case "Mob":
                    items.AddRange(mobBack.Keys);
                    break;
                case "Npc":
                    items.AddRange(npcBack.Keys);
                    break;
                case "BGM":
                    items.AddRange(bgmBack.Keys);
                    break;
            }

            Parallel.ForEach(items, item =>
            {
                lock (locker)
                {
                    var searchQuery = item.Contains('-') ? item.Remove(item.IndexOf('-') - 1) : item.Remove(item.IndexOf('(') - 1);
                    Parallel.ForEach(_infos, map =>
                    {
                        lock (locker2)
                        {
                            if (map.Value.ContainsItem(searchQuery)) removeList.Add(item);
                        }
                    });
                }
            });

            foreach (var a in removeList) items.Remove(a);
            radListControl2.Items.AddRange(items);

        }

        private void radDropDownList2_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            var searchResults = radListControl2.Items.Select(item => item.Text)
                .ToDictionary(d => d,
                    d => int.Parse(Regex.Match(d.Substring(d.IndexOf('(')).Replace(",", ""), @"\d+").Value));

            if (searchResults.Count != 0) radListControl2.Items.Clear();

            if (this.radDropDownList2.Text == Strings.OrderSize)
            {
                objBack = objBack.OrderBy(o => o.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
                backBack = backBack.OrderBy(o => o.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
                tileBack = tileBack.OrderBy(o => o.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
                reactorBack = reactorBack.OrderBy(o => o.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
                mobBack = mobBack.OrderBy(o => o.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
                npcBack = npcBack.OrderBy(o => o.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
                bgmBack = bgmBack.OrderBy(o => o.Value).Reverse().ToDictionary(d => d.Key, d => d.Value);
                if (searchResults.Count != 0) radListControl2.Items.AddRange(searchResults.OrderBy(d => d.Value).Reverse().Select(d => d.Key));
            }
            else
            {
                objBack = objBack.OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);
                backBack = backBack.OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);
                tileBack = tileBack.OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);
                reactorBack = reactorBack.OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);
                mobBack = mobBack.OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);
                npcBack = npcBack.OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);
                bgmBack = bgmBack.OrderBy(o => o.Key).ToDictionary(d => d.Key, d => d.Value);
                if (searchResults.Count != 0) radListControl2.Items.AddRange(searchResults.OrderBy(d => d.Key).Select(d => d.Key));
            }
            radDropDownList1_SelectedIndexChanged(sender, e);
        }
    }
}
