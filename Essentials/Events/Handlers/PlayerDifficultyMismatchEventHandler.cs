using Frostspark.API.Events;
using Frostspark.API.Events.Players;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DifficultyResult = Frostspark.API.Events.Players.PlayerDifficultyMismatchEvent.DifficultyResult;

namespace Essentials.Events.Handlers
{
    internal class PlayerDifficultyMismatchEventHandler : SyncEventHandler<PlayerDifficultyMismatchEvent>
    {
        public override void Handle(PlayerDifficultyMismatchEvent obj)
        {
            obj.Result = DifficultyResult.Allow;
        }
    }
}
