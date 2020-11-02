using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

namespace Essentials.Commands.Implementations
{
    [CommandName("spawnrate")]
    [CommandDescription("Modifies the server-wide spawnrate.")]
    [CommandPermission("essentials.commands.spawnrate")]
    internal class SpawnrateCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void GetRate()
        {
            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

            Sender.SendFormattedMessage($"The current global spawnrate is {emph}{Terraria.NPC.defaultSpawnRate}{infocol}.", infocol);
        }

        [CommandCallback]
        public void SetRate(int rate)
        {
            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

            Terraria.NPC.defaultSpawnRate = rate;

            Sender.SendFormattedMessage($"The global spawnrate has been updated to {emph}{rate}{infocol}.", infocol);
        }
    }
}
