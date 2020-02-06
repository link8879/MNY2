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
using System.Collections.Generic;

namespace MNY2
{
    public class MapInfo
    {
        public int Id { get; set; }
        public int Size { get; set; }
#nullable enable
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Tiles { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Objects { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Backgrounds { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Reactors { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<int>? Monsters { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<int>? NPCs { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BGM { get; set; } = "";
#nullable disable

        public bool ContainsItem(string item)
        {
            if (Tiles != null)
                if (Tiles.Contains(item)) return true;
            if (Objects != null)
                if (Objects.Contains(item)) return true;
            if (Backgrounds != null)
                if (Backgrounds.Contains(item)) return true;
            if (Reactors != null)
                if (Reactors.Contains(item)) return true;
            if (int.TryParse(item, out _))
            {
                if (Monsters != null)
                    if (Monsters.Contains(int.Parse(item)))
                        return true;
                if (NPCs != null)
                    if (NPCs.Contains(int.Parse(item)))
                        return true;
            }
            return BGM == item;
        }
    }
}
