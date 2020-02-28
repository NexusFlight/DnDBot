using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBot
{
    public class MagicItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public string Value { get; set; }
        public string Attunement { get; set; }
    }

    public class RootObject
    {
        public List<MagicItem> MagicItems { get; set; }
    }
}
