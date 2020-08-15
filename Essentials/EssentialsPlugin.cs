using Essentials.Commands;
using Essentials.Commands.Implementations;

using Frostspark.API;

using Microsoft.Xna.Framework;

using System;
using System.Runtime.CompilerServices;

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

        public override void Disable()
        {
            Commands.DeregisterCommands();
        }

        public override void Enable()
        {
            Commands.RegisterCommands();
        }

        public override void Load()
        {
            Commands = new CommandManager(this);
        }

        public override void Unload()
        {
            Commands = null;
        }

        internal static Frostspark.Server.Server Server => (Frostspark.Server.Server)Frostspark.API.Frostspark.Server;
    }
}
