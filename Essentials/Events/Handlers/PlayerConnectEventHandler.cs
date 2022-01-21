using Frostspark.API.Events;
using Frostspark.API.Events.Players;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Events.Handlers
{
    internal class PlayerConnectEventHandler : SyncEventHandler<PlayerConnectEvent>
    {
        public override void Handle(PlayerConnectEvent obj)
        {
            if (EssentialsPlugin.Instance.Whitelist.Enabled)
            {
                var ply = obj.Player as Frostspark.Server.Entities.Player;

                if (!EssentialsPlugin.Instance.Whitelist.Contains((ply.Socket.RemoteAddress as IPEndPoint).Address))
                {
                    obj.Result = PlayerConnectEvent.ConnectResult.KickWhitelist;
                }
            }
        }
    }
}
