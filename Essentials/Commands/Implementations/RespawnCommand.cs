using Frostspark.Server.Commands.Assertions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

namespace Essentials.Commands.Implementations
{
    [CommandName("respawn")]
    [CommandDescription("Respawns a player")]
    [CommandPermission("essentials.commands.respawn")]
    public class RespawnCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void RespawnSelf()
        {
            if (!EntityAssertions.Assert_SenderPlayer(Sender, out var ply))
                return;

            ply.Respawn();

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            ply.SendFormattedMessage($"You respawned {emphasis}yourself{infocol}.", infocol);
        }

        [CommandCallback]
        public void RespawnPlayer(Player ply)
        {
            ply.Respawn();

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            Sender.SendFormattedMessage($"You respawned {emphasis}{ply.Name}{infocol}.", infocol);
            ply.SendFormattedMessage($"You were respawned by {emphasis}{Sender.LogName}{infocol}.", infocol);
        }
    }
}
