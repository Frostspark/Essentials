using Essentials.Commands;
using Essentials.Commands.Implementations;

using Frostspark.API;

using Microsoft.Xna.Framework;

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Terraria;

namespace Essentials
{
    public class EssentialsPlugin : Frostspark.API.Plugins.Plugin
    {
        internal static EssentialsPlugin Instance { get; private set; }

        internal CommandManager Commands { get; private set; }

        public EssentialsPlugin()
        {
            Instance = this;
        }

        public override string Name => "Essentials";

        public override string Author => "quake1337";

        public override Task Disable()
        {
            Commands.DeregisterCommands();

            return Task.CompletedTask;
        }

        public override Task Enable()
        {
            Commands.RegisterCommands();

            return Task.CompletedTask;
        }

        public override Task Load()
        {
            Commands = new CommandManager(this);

            return Task.CompletedTask;
        }

        public override Task Unload()
        {
            Commands = null;

            return Task.CompletedTask;
        }

        internal static Frostspark.Server.Server Server => (Frostspark.Server.Server)Frostspark.API.Frostspark.Server;
    }
}
