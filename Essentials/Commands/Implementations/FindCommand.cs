using Essentials.Enums;

using Frostspark.API.Structures;
using Frostspark.API.Utilities;
using Frostspark.Server;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Commands.Parsers;
using Frostspark.Server.Entities;
using Frostspark.Server.GameData;
using Frostspark.Server.Utilities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;

namespace Essentials.Commands.Implementations
{
    [CommandName("find")]
    [CommandDescription("Searches game data for specific values")]
    [CommandPermission("essentials.commands.find")]
    public class FindCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void Find(SearchType type, string query, int page = 1)
        {
            switch(type)
            {
                case SearchType.Item:
                    FindItem(query, page);
                    break;
                case SearchType.Paint:
                    FindPaint(query, page);
                    break;
                case SearchType.Tile:
                    FindTile(query, page);
                    break;
                case SearchType.Wall:
                    FindWall(query, page);
                    break;
                default:
                    Sender.SendErrorMessage($"The search type {type} is currently not implemented.");
                    break;
            }
        }

        [SubCommand("fromitem")]
        public void FindFromItem(SearchType type, [ItemID] int query, int page = 1)
        {
            Sender.SendInfoMessage($"FromItem {query} find typo {type} (p {page})");
        }

        private void FindTile(string query, int page)
        {
            var colors = Server.Instance.Colors;

            FindItemProperty(query, page, (i) => i.createTile is not -1, (p, mp) => $"Tile search ({Color.Orange}search \"{Color.OrangeRed}{query}{Color.Orange}\", page {p}/{mp})", (p) => $"{Color.IndianRed}/find {Color.LightGray}tile \"{query}\" {p}", (item) =>
            {
                Sender.SendFormattedMessage($"Tile {item.createTile} ({ReflectionUtilities.FindConstantByValue<TileID>(item.createTile)}) placed by item {new NetItem() { Type = item.type, Prefix = 0, Stack = 1 }} ({item.type})", colors.Info);
            });
        }

        private void FindWall(string query, int page)
        {
            var colors = Server.Instance.Colors;

            FindItemProperty(query, page, (i) => i.createWall is not -1, (p, mp) => $"Wall search ({Color.Orange}search \"{Color.OrangeRed}{query}{Color.Orange}\", page {p}/{mp})", (p) => $"{Color.IndianRed}/find {Color.LightGray}wall \"{query}\" {p}", (item) =>
            {
                Sender.SendFormattedMessage($"Wall {item.createWall} ({ReflectionUtilities.FindConstantByValue<WallID>(item.createWall)}) placed by item {new NetItem() { Type = item.type, Prefix = 0, Stack = 1 }} ({item.type})", colors.Info);
            });
        }

        private void FindPaint(string query, int page)
        {
            var colors = Server.Instance.Colors;

            FindItemProperty(query, page, (i) => i.paint is not 0, (p, mp) => $"Paint search ({Color.Orange}search \"{Color.OrangeRed}{query}{Color.Orange}\", page {p}/{mp})", (p) => $"{Color.IndianRed}/find {Color.LightGray}paint \"{query}\" {p}", (item) =>
            {
                Sender.SendFormattedMessage($"Paint {item.paint} ({ReflectionUtilities.FindConstantByValue(typeof(PaintID), item.paint)}) created by item {new NetItem() { Type = item.type, Prefix = 0, Stack = 1 }} ({item.type})", colors.Info);
            });
        }

        private void FindItem(string query, int page)
        {
            var colors = Server.Instance.Colors;

            FindItemProperty(query, page, (i) => true, (p, mp) => $"Item search ({Color.Orange}search \"{Color.OrangeRed}{query}{Color.Orange}\", page {p}/{mp})", (p) => $"{Color.IndianRed}/find {Color.LightGray}item \"{query}\" {p}", (item) =>
            {
                Sender.SendFormattedMessage($"Item {item.Name}: {new NetItem() { Type = item.type, Prefix = 0, Stack = 1 }} ({item.type})", colors.Info);
            });
        }

        private void FindItemProperty(string query, int page, Func<Terraria.Item, bool> predicate, Func<int, int, FormattableString> page_header_formatter, Func<int, FormattableString> page_footer_formatter, Action<Terraria.Item> printer)
        {
            string normalised_query = query.ToLower().Replace(" ", "");

            List<Terraria.Item> items = new();

            foreach (var item in GameContentDictionaries.Items.Values)
            {
                if (item.Name.ToLower().Replace(" ", "").Contains(normalised_query) && predicate(item))
                {
                    items.Add(item);
                }
            }

            Paginate(items, Sender, printer, page_header_formatter, page_footer_formatter, page);
        }

        private static void Paginate<T>(IList<T> entries, CommandSender sender, Action<T> printer, Func<int, int, FormattableString> page_header_formatter, Func<int, FormattableString> page_footer_formatter, int page)
        {
            var colors = Server.Instance.Colors;

            int maxpage = PaginationUtilities.DeterminePageCount(entries.Count, 7);

            if (!PaginationUtilities.IsPageValid(entries.Count, page - 1, 7))
            {
                sender.SendMessage($"This page is empty. Try pages {1} - {maxpage}", colors.ErrorPale);
                return;
            }

            sender.SendFormattedMessage(page_header_formatter(page, maxpage), colors.Info);

            foreach (var item in PaginationUtilities.Paginate(entries, page - 1))
            {
                printer(item);
            }

            if (page > 1)
            {
                sender.SendFormattedMessage($"> Use {page_footer_formatter(page - 1)} {page - 1}{colors.Info} for the previous page.", colors.Info);
            }

            if (page < maxpage)
            {
                sender.SendFormattedMessage($"> Use {page_footer_formatter(page + 1)}{colors.Info} for the next page.", colors.Info);
            }
        }
    }
}
