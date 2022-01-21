using Essentials.Commands;
using Essentials.Commands.Implementations;
using Essentials.Events;
using Essentials.Whitelisting;

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

        internal EventHandlerManager Events { get; private set; }

        internal Whitelist Whitelist { get; private set; }

        public EssentialsPlugin()
        {
            Instance = this;
        }

        public override string Name => "Essentials";

        public override string Author => "quake1337";

        public override async Task Disable()
        {
            Events.Unregister();
            Commands.DeregisterCommands();
            await Whitelist.SaveFile();
        }

        public override async Task Enable()
        {
            Events.Register();
            Commands.RegisterCommands();
            await Whitelist.LoadFile();
        }

        public override Task Load()
        {
            Commands = new CommandManager(this);
            Events = new(this);
            Whitelist = new(this);

            return Task.CompletedTask;
        }

        public override Task Unload()
        {
            Commands = null;
            Events = null;
            Whitelist = null;

            return Task.CompletedTask;
        }

        internal static Frostspark.Server.Server Server => (Frostspark.Server.Server)Frostspark.API.Frostspark.Server;
    }
}
