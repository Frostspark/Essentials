
using Frostspark.API;
using Frostspark.API.Utilities;
using Frostspark.API.Utilities.Extensions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Essentials.Commands.Implementations
{
    [CommandName("who", "online")]
    [CommandDescription("Lists players connected to the server.")]
    [CommandPermission("essentials.commands.who")]
    public class WhoCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void ShowOnlinePlayers()
        {
            var srv = EssentialsPlugin.Server;

            var players = srv.Players.Search(x => true);

            var infocol = srv.Colors.Info;

            Sender.SendFormattedMessage($"There are {players.Count}/{Terraria.Main.maxNetPlayers} players currently online:", infocol);

            if (players.Count > 0)
                Sender.SendFormattedMessage($"{FormattableStringExtensions.Join(", ", players.Select(ply => ColorPlayer(ply)))}", infocol);
        }

        private FormattableString ColorPlayer(Player ply)
        {
            return $"{Color.OrangeRed}{ply.Name}{Color.Yellow}";
        }
    }
}
