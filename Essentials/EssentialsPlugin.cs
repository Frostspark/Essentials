using Essentials.Commands;

using System;
using System.Runtime.CompilerServices;

namespace Essentials
{
    public class EssentialsPlugin : Frostspark.API.Plugins.Plugin
    {
        internal static EssentialsPlugin Instance { get; private set; }

        public EssentialsPlugin()
        {
            Instance = this;
        }

        internal Frostspark.Server.Server NServer => (Frostspark.Server.Server)Server;

        public override string Name => "Essentials";

        public override string Author => "quake1337";

        public override void Disable()
        {
            NServer.Commands.DeregisterCommand<WhoCommand>();
        }

        public override void Enable()
        {
            NServer.Commands.RegisterCommand<WhoCommand>();
        }

        public override void Load()
        {
            
        }

        public override void Unload()
        {
            
        }
    }
}
