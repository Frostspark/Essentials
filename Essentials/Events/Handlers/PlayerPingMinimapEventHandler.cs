using Frostspark.API.Enums;
using Frostspark.API.Events;
using Frostspark.API.Events.Players;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Events.Handlers
{
    internal class PlayerPingMinimapEventHandler : SyncEventHandler<PlayerPingMinimapEvent>
    {
        public override void Handle(PlayerPingMinimapEvent obj)
        {
            obj.Cancelled = true;
            obj.Player.TeleportToPos(obj.Position.X, obj.Position.Y, new() { Type = TeleportEffectType.MagicMirrorTeleport });
        }
    }
}
