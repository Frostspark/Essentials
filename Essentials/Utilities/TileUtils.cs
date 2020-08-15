using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;

using Terraria;

namespace Essentials.Utilities
{
    public static class TileUtils
    {
        public static (int x, int y) GetRandomClearTile(int origin_x, int origin_y, int x_range, int y_range)
        {
            int attempt = 0;

            int upper_x_range = Math.Min(origin_x + x_range, Main.maxTilesX) - origin_x;
            int upper_y_range = Math.Min(origin_y + y_range, Main.maxTilesY) - origin_y;

            int lower_x_range = Math.Max(origin_x - x_range, 0) - origin_x;
            int lower_y_range = Math.Max(origin_y - y_range, 0) - origin_y;

            while (attempt < 100)
            {
                int x = origin_x + Main.rand.Next(lower_x_range, upper_x_range + 1);
                int y = origin_y + Main.rand.Next(lower_y_range, upper_y_range + 1);

                if (!IsTileSolid(x, y))
                {
                    return (x, y);
                }

                attempt++;
            }

            return (origin_x, origin_y);
        }

        public static bool IsTileSolid(int x, int y)
        {
            var tile = Main.tile[x, y];

            return tile != null && tile.active() && Main.tileSolid[tile.type] && !tile.inActive() && !tile.halfBrick() && tile.slope() == 0;
        }
    }
}
