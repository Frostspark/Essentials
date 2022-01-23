using Essentials.Events.Handlers;

using Frostspark.Server;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Events
{
    internal class EventHandlerManager
    {
        private readonly EssentialsPlugin Plugin;
        private readonly PlayerConnectEventHandler ConnectHandler = new();
        private readonly PlayerDifficultyMismatchEventHandler DifficultyMismatchHandler = new();
        private readonly PlayerPingMinimapEventHandler PingMinimapHandler = new();

        public EventHandlerManager(EssentialsPlugin plugin) => Plugin = plugin;

        internal void Register()
        {
            Server.Instance.Events.RegisterHandler(Plugin, ConnectHandler);
            Server.Instance.Events.RegisterHandler(Plugin, DifficultyMismatchHandler);
            Server.Instance.Events.RegisterHandler(Plugin, PingMinimapHandler);
        }

        internal void Unregister()
        {
            Server.Instance.Events.UnregisterHandler(Plugin, ConnectHandler);
            Server.Instance.Events.UnregisterHandler(Plugin, DifficultyMismatchHandler);
            Server.Instance.Events.UnregisterHandler(Plugin, PingMinimapHandler);
        }
    }
}
