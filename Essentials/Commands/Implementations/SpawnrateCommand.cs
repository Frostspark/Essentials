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

        }

        [CommandCallback]
        public void SetRate(int rate)
        {

        }
    }
}
