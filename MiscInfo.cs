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

using System.Collections.Generic;

namespace MNY2
{
    class MiscInfo
    {
        public List<string> Sounds { get; set; } = new List<string>();
        public List<string> Tiles { get; set; }
        public List<string> Objects { get; set; }
        public List<string> Backgrounds { get; set; }
        public List<string> Reactors { get; set; } = new List<string>();
        public Dictionary<int, string> MapNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> MobNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> NpcNames { get; set; } = new Dictionary<int, string>();
        public List<int> Monsters { get; set; } = new List<int>();
        public List<int> Npcs { get; set; } = new List<int>();
    }
}
