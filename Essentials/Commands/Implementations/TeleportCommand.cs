
using Frostspark.API.Utilities;
using Frostspark.Server.Commands.Assertions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;
using Frostspark.Server.Utilities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Essentials.Commands.Implementations
{
    [CommandName("tp", "teleport")]
    [CommandDescription("Teleports an entity to a given location")]
    public class TeleportCommand : CommandWrapper<CommandSender>
    {
        [SubCommand("player", "ply")]
        public class PlayerTargeted : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void SelfToPlayer(Player target)
            {
                if (!EntityAssertions.Assert_SenderTeleportable(Sender, out var itp))
                    return;

                itp.TeleportToEntity(target);

                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

                Sender.SendFormattedMessage($"You have been teleported to {emphasis}{target.Name}{infocol}.", infocol);

                EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Teleported to {target.Name}.");
            }

            [CommandCallback]
            public void PlayerToPlayer(Player teleportee, Player destination)
            {
                teleportee.TeleportToEntity(destination);

                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

                Sender.SendFormattedMessage($"You teleported {emphasis}{teleportee.Name}{infocol} to {emphasis}{destination.Name}{infocol}.", infocol);

                teleportee.SendFormattedMessage($"You were teleported to {emphasis}{destination.Name}{infocol} by {emphasis}{Sender.LogName}{infocol}.", infocol);

                EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Teleported {teleportee.Name} to {destination.Name}.");
            }
        }

        [SubCommand("pos")]
        public class TileTargeted : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void SelfToPos(int x, int y)
            {
                if (!EntityAssertions.Assert_SenderTeleportable(Sender, out var itp))
                    return;

                itp.TeleportToTile(x, y);

                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

                Sender.SendFormattedMessage($"You have been teleported to {emphasis}{x}, {y}{infocol}.", infocol);

                EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Teleported to {x}, {y}.");
            }

            public void PlayerToPos(Player player, int x, int y)
            {
                player.TeleportToTile(x, y);

                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

                player.SendFormattedMessage($"You have been teleported to {emphasis}{x}, {y}{infocol} by {emphasis}{Sender.LogName}{infocol}.", infocol);
                Sender.SendFormattedMessage($"You teleported {emphasis}{player.Name}{infocol} to {emphasis}{x}, {y}{infocol}", infocol);

                EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Teleported {player.Name} to {x}, {y}.");
            }
        }
    }
}
