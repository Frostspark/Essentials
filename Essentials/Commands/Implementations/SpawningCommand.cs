using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

namespace Essentials.Commands.Implementations
{
    [CommandName("spawning")]
    [CommandDescription("Modifies the server-wide NPC spawning settings.")]
    [CommandPermission("essentials.commands.spawning")]
    internal class SpawningCommand : CommandWrapper<CommandSender>
    {
        [SubCommand("rate")]
        [CommandPermission("essentials.commands.spawning.rate")]
        public class SpawningRateCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void GetRate()
            {
                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                Sender.SendFormattedMessage($"The current spawnrate setting is {emph}{Terraria.NPC.defaultSpawnRate}{infocol}.", infocol);
            }

            [CommandCallback]
            public void SetRate(int rate)
            {
                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                Terraria.NPC.defaultSpawnRate = rate;

                Sender.SendFormattedMessage($"The spawnrate setting has been updated to {emph}{rate}{infocol}.", infocol);

                EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Updated the spawnrate to {rate}.");
            }
        }

        [SubCommand("max")]
        [CommandPermission("essentials.commands.spawning.max")]
        public class SpawningMaxCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void GetRate()
            {
                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                Sender.SendFormattedMessage($"The current max-spawns setting is {emph}{Terraria.NPC.defaultMaxSpawns}{infocol}.", infocol);
            }

            [CommandCallback]
            public void SetRate(int rate)
            {
                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                Terraria.NPC.defaultMaxSpawns = rate;

                Sender.SendFormattedMessage($"The max-spawns setting has been updated to {emph}{rate}{infocol}.", infocol);

                EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Updated max-spawns to {rate}.");
            }
        }
    }
}
