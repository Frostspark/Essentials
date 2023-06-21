using Essentials.Commands.Implementations;
using Essentials.Commands.Parsers;

using System;
using System.Collections.Generic;
using System.Text;

namespace Essentials.Commands
{
    public class CommandManager
    {
        private readonly EssentialsPlugin Instance;

        public CommandManager(EssentialsPlugin instance)
        {
            Instance = instance;
        }

        public void RegisterCommands()
        {
            var cmds = EssentialsPlugin.Server.Commands;

            cmds.RegisterCommand<FindCommand>();
            cmds.RegisterCommand<GiveCommand>();
            cmds.RegisterCommand<GodCommand>();
            cmds.RegisterCommand<ItemCommand>();
            cmds.RegisterCommand<RespawnCommand>();
            cmds.RegisterCommand<SpawnMobCommand>();
            cmds.RegisterCommand<TeleportCommand>();
            cmds.RegisterCommand<WhitelistCommand>();
            cmds.RegisterCommand<WhoCommand>();
            cmds.RegisterCommand<WorldCommand>();
            
            cmds.RegisterParser<IPAddressParser>();
            cmds.RegisterParser<WeatherTypeParser>();
            cmds.RegisterParser<SearchTypeParser>();
        }

        public void DeregisterCommands()
        {
            var cmds = EssentialsPlugin.Server.Commands;

            cmds.DeregisterCommand<FindCommand>();
            cmds.DeregisterCommand<GiveCommand>();
            cmds.DeregisterCommand<GodCommand>();
            cmds.DeregisterCommand<ItemCommand>();
            cmds.DeregisterCommand<RespawnCommand>();
            cmds.DeregisterCommand<SpawnMobCommand>();
            cmds.DeregisterCommand<TeleportCommand>();
            cmds.DeregisterCommand<WhitelistCommand>();
            cmds.DeregisterCommand<WhoCommand>();
            cmds.DeregisterCommand<WorldCommand>();

            cmds.DeregisterParser<IPAddressParser>();
            cmds.DeregisterParser<WeatherTypeParser>();
            cmds.DeregisterParser<SearchTypeParser>();
        }

    }
}
