using Essentials.Commands.Implementations;

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

            cmds.RegisterCommand<GiveCommand>();
            cmds.RegisterCommand<ItemCommand>();
            cmds.RegisterCommand<SpawnMobCommand>();
            cmds.RegisterCommand<SpawningCommand>();
            cmds.RegisterCommand<TeleportCommand>();
            cmds.RegisterCommand<WhoCommand>();
            cmds.RegisterCommand<RespawnCommand>();
            cmds.RegisterCommand<GodCommand>();
        }

        public void DeregisterCommands()
        {
            var cmds = EssentialsPlugin.Server.Commands;

            cmds.DeregisterCommand<GiveCommand>();
            cmds.DeregisterCommand<ItemCommand>();
            cmds.DeregisterCommand<SpawnMobCommand>();
            cmds.DeregisterCommand<SpawningCommand>();
            cmds.DeregisterCommand<TeleportCommand>();
            cmds.DeregisterCommand<WhoCommand>();
            cmds.DeregisterCommand<RespawnCommand>();
            cmds.DeregisterCommand<GodCommand>();
        }

    }
}
