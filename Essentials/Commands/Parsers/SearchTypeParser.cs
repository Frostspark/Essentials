using Essentials.Enums;

using SharedUtils.Commands.Attributes.Parsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Commands.Parsers
{
    internal class SearchTypeParser
    {
        [TypeParser(typeof(SearchType))]
        public static SearchType ParseWeatherType(string s)
        {
            return s.ToLower() switch
            {
                "tile" or "-tile" => SearchType.Tile,
                "npc" or "-npc" => SearchType.NPC,
                "item" or "-item" => SearchType.Item,
                "wall" or "-wall" => SearchType.Wall,
                "projectile" or "-projectile" => SearchType.Projectile,
                "paint" or "-paint" => SearchType.Paint,
                _ => throw new FormatException("Unknown search type.")
            };
        }
    }
}
