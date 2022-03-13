using Frostspark.API.Utilities;
using Frostspark.Server;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Commands.Implementations
{
    [CommandName("whitelist", "wl")]
    [CommandDescription("Manages the server's whitelist.")]
    [CommandPermission("essentials.commands.whitelist")]
    public class WhitelistCommand : CommandWrapper<CommandSender>
    {
        [SubCommand("status")]
        public void Status()
        {
            Sender.SendInfoMessage($"Whitelist is {(EssentialsPlugin.Instance.Whitelist.Enabled ? "enabled" : "disabled")}");
            Sender.SendInfoMessage($"There are {EssentialsPlugin.Instance.Whitelist.Allowed.Count} whitelisted addresses.");
        }

        [SubCommand("add", "a")]
        public async Task Add(IPAddress ip)
        {
            EssentialsPlugin.Instance.Whitelist.Add(ip);
            await EssentialsPlugin.Instance.Whitelist.SaveFile();

            Sender.SendSuccessMessage($"Added {ip} to the whitelist.");
        }

        [SubCommand("list", "ls", "l")]
        public void List(int page = 1)
        {
            var list = EssentialsPlugin.Instance.Whitelist.Allowed;

            var col = Server.Instance.Colors;

            int maxpage = PaginationUtilities.DeterminePageCount(list.Count);

            if (page < 1 || page > maxpage)
            {
                Sender.SendMessage($"This page is empty. Try pages {1} - {maxpage}", col.Error);
            }
            else
            {
                Sender.SendInfoMessage($"Page {page}/{maxpage} of whitelist");

                //The method takes a zero-based page number, but the one provided by the user is 1-based.

                foreach (var ip in PaginationUtilities.Paginate(list, page - 1))
                {
                    Sender.SendInfoMessage($"- {ip}");
                }

                if (page > 1)
                {
                    Sender.SendFormattedMessage($"> Use {Color.IndianRed}/whitelist {Color.LightGray}list {page - 1}{col.Info} for the previous page.", col.Info);
                }

                if (page < maxpage)
                {
                    Sender.SendFormattedMessage($"> Use {Color.IndianRed}/whitelist {Color.LightGray}list {page + 1}{col.Info} for the next page.", col.Info);
                }
            }
        }

        [SubCommand("remove", "rm", "rem", "r")]
        public async Task Remove(IPAddress ip)
        {
            EssentialsPlugin.Instance.Whitelist.Remove(ip);
            await EssentialsPlugin.Instance.Whitelist.SaveFile();

            Sender.SendSuccessMessage($"Removed {ip} from the whitelist.");
        }

        [SubCommand("enable", "enabled", "on")]
        public async Task Enable()
        {
            EssentialsPlugin.Instance.Whitelist.Enabled = true;
            await EssentialsPlugin.Instance.Whitelist.SaveFile();

            Server.Instance.Commands.LogCommandActivity(Sender, "Enabled the whitelist");
        }

        [SubCommand("disable", "disabled", "off")]
        public async Task Disable()
        {
            EssentialsPlugin.Instance.Whitelist.Enabled = false;
            await EssentialsPlugin.Instance.Whitelist.SaveFile();

            Server.Instance.Commands.LogCommandActivity(Sender, "Disabled the whitelist");
        }
    }
}
