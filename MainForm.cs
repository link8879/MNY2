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
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;

namespace MNY2
{
    public partial class MainForm : RadForm
    {
        private Dictionary<string, MapInfo> _infos = new Dictionary<string, MapInfo>();
        private List<string> mobBack = new List<string>();
        private List<string> npcBack = new List<string>();
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

            foreach (var a in miscInfo.Monsters)
            {
                var name = miscInfo.MobNames.ContainsKey(a) ? miscInfo.MobNames[a] : Strings.NoName;
                mobBack.Add($"{a} - {name}");
            }

            foreach (var a in miscInfo.Npcs)
            {
                var name = miscInfo.NpcNames.ContainsKey(a) ? miscInfo.NpcNames[a] : Strings.NoName;
                npcBack.Add($"{a} - {name}");
            }
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
                Clipboard.SetText(list.SelectedItem.Text.Contains('-') ? list.SelectedItem.Text.Remove(list.SelectedItem.Text.IndexOf('-') - 1) : list.SelectedItem.Text);
                MessageBox.Show(Strings.Copied, Strings.Inform);
            }
        }

        private void mapList_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            if (e.Position < 0) return;
            var searchQuery = radListControl1.Items.ToArray()[e.Position].Text;
            if (searchQuery.Contains('-')) searchQuery = searchQuery.Remove(searchQuery.IndexOf('-') - 1);
            radListControl2.Items.Clear();
            foreach (var item in _infos.Where(item => item.Value.ContainsItem(searchQuery)))
            {
                var name = miscInfo.MapNames.ContainsKey(int.Parse(item.Key)) ? miscInfo.MapNames[int.Parse(item.Key)] : Strings.NoName;
                radListControl2.Items.Add($"{item.Key} - {name}");
            }
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            if (miscInfo == null || _infos.Count == 0) return;

            radListControl1.Items.Clear();

            switch (radDropDownList1.Text)
            {
                case "Obj":
                    radListControl1.Items.AddRange(miscInfo.Objects);
                    break;
                case "Reactor":
                    radListControl1.Items.AddRange(miscInfo.Reactors);
                    break;
                case "Back":
                    radListControl1.Items.AddRange(miscInfo.Backgrounds);
                    break;
                case "Tile":
                    radListControl1.Items.AddRange(miscInfo.Tiles);
                    break;
                case "Mob":
                    radListControl1.Items.AddRange(mobBack);
                    break;
                case "Npc":
                    radListControl1.Items.AddRange(npcBack);
                    break;
                case "BGM":
                    radListControl1.Items.AddRange(miscInfo.Sounds);
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
                    items.AddRange(miscInfo.Objects);
                    break;
                case "Reactor":
                    items.AddRange(miscInfo.Reactors);
                    break;
                case "Back":
                    items.AddRange(miscInfo.Backgrounds);
                    break;
                case "Tile":
                    items.AddRange(miscInfo.Tiles);
                    break;
                case "Mob":
                    items.AddRange(mobBack);
                    break;
                case "Npc":
                    items.AddRange(npcBack);
                    break;
                case "BGM":
                    items.AddRange(miscInfo.Sounds);
                    break;
            }

            Parallel.ForEach(items, item =>
            {
                lock (locker)
                {
                    var searchQuery = item.Contains('-') ? item.Remove(item.IndexOf('-') - 1) : item;
                    Parallel.ForEach(_infos, map =>
                    {
                        lock (locker2)
                        {
                            if(map.Value.ContainsItem(searchQuery)) removeList.Add(item);
                        }
                    });
                }
            });

            foreach (var a in removeList) items.Remove(a);
            radListControl2.Items.AddRange(items);
        }
    }
}
