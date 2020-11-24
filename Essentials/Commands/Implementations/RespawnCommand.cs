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
            if (!EntityAssertions.Assert_SenderPlayer(Sender, out var ply) || !Assert_PlayerDead(Sender, ply))
                return;

            ply.Respawn();

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            ply.SendFormattedMessage($"You respawned {emphasis}yourself{infocol}.", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Respawned self.");
        }

        [CommandCallback]
        public void RespawnPlayer(Player ply)
        {
            if (!Assert_PlayerDead(Sender, ply))
                return;

            ply.Respawn();

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            Sender.SendFormattedMessage($"You respawned {emphasis}{ply.Name}{infocol}.", infocol);
            ply.SendFormattedMessage($"You were respawned by {emphasis}{Sender.LogName}{infocol}.", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Respawned {ply.Name}.");
        }

        private bool Assert_PlayerDead(CommandSender sender, Player player)
        {
            if(!player.Handle.dead)
            {
                if (sender == player)
                    sender.SendErrorMessage($"You don't seem to be dead.");
                else
                    sender.SendErrorMessage($"{player.Name} doesn't seem to be dead.");

                return false;
            }

            return true;
        }
    }
}
