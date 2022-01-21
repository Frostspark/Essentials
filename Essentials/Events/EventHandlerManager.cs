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

        public EventHandlerManager(EssentialsPlugin plugin) => Plugin = plugin;

        internal void Register()
        {
            Server.Instance.Events.RegisterHandler(Plugin, ConnectHandler);
        }

        internal void Unregister()
        {
            Server.Instance.Events.UnregisterHandler(Plugin, ConnectHandler);
        }
    }
}
