using Frostspark.API;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

namespace Essentials.Commands
{
    [CommandName("who", "online")]
    public class WhoCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void ShowOnlinePlayers()
        {
            
        }
    }
}
