using Frostspark.API.Enums;
using Frostspark.Server;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Commands.Implementations
{
    [CommandName("world")]
    [CommandDescription("Manages world information")]
    [CommandPermission("essentials.commands.world")]
    public class WorldCommand : CommandWrapper<CommandSender>
    {
        private static Server Server => EssentialsPlugin.Server;

        [SubCommand("difficulty", "diff", "mode")]
        [CommandPermission("essentials.commands.world.difficulty")]
        public class WorldDifficultyCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void Get()
            {
                var colors = Server.Colors;
                var difficulty = Server.World.Difficulty;

                Sender.SendFormattedMessage($"Current world difficulty: {colors.TargetEmphasis}{difficulty}{colors.Info}.", colors.Info);
            }

            [CommandCallback]
            public void Set(WorldDifficulty new_difficulty)
            {
                var colors = Server.Colors;
                var old_diff = Server.World.Difficulty;

                Server.World.Difficulty = new_difficulty;

                Sender.SendFormattedMessage($"Changed world difficulty from {colors.TargetEmphasis}{old_diff}{colors.Success} to {colors.TargetEmphasis}{new_difficulty}{colors.Success}.", colors.Success);

                Server.Commands.LogCommandActivity(Sender, $"Changed world difficulty from {old_diff} to {new_difficulty}.");

            }
        }

        [SubCommand("name", "rename")]
        [CommandPermission("essentials.commands.world.name")]
        public class WorldNameCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void Get()
            {
                var colors = Server.Colors;
                var name = Server.World.Name;

                Sender.SendFormattedMessage($"Current world name: {colors.TargetEmphasis}{name}{colors.Info}", colors.Info);
            }

            [CommandCallback]
            public void Set(string name)
            {
                var colors = Server.Colors;
                var old_name = Server.World.Name;

                Server.World.Name = name;

                Sender.SendFormattedMessage($"Changed world name from {colors.TargetEmphasis}{old_name}{colors.Success} to {colors.TargetEmphasis}{name}{colors.Success}.", colors.Success);
            }
        }

        [SubCommand("hardmode")]
        [CommandPermission("essentials.commands.world.hardmode")]
        public class WorldHardmodeCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void Get()
            {
                var colors = Server.Colors;

                var value = Server.World.Hardmode;

                Sender.SendFormattedMessage($"Hardmode is {(value ? $"{colors.Success}enabled " : $"{colors.Error}disabled")}{colors.Info}.", colors.Info);
            }

            [CommandCallback]
            public void Set(bool state)
            {
                var colors = Server.Colors;

                Server.World.Hardmode = state;

                Sender.SendFormattedMessage($"Hardmode has been {(state ? $"{colors.Success}enabled " : $"{colors.Error}disabled")}{colors.Info}.", colors.Info);
            }
        }
    }
}
