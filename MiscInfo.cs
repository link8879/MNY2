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
        public Dictionary<string, int> Sounds { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Tiles { get; set; }
        public Dictionary<string, int> Objects { get; set; }
        public Dictionary<string, int> Backgrounds { get; set; }
        public Dictionary<string, int> Reactors { get; set; } = new Dictionary<string, int>();
        public Dictionary<int, string> MapNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> MobNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> NpcNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, int> Monsters { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> Npcs { get; set; } = new Dictionary<int, int>();
    }
}
