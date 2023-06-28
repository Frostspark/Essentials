using Frostspark.Server.Commands.Assertions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Commands.Parsers;
using Frostspark.Server.Entities;

using Microsoft.Xna.Framework;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

using Terraria;
using Terraria.DataStructures;

using Player = Frostspark.Server.Entities.Player;

namespace Essentials.Commands.Implementations
{
    [CommandName("give")]
    [CommandDescription("Gives an item to a player")]
    [CommandPermission("essentials.commands.give")]
    public class GiveCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void SpawnItem(Player player, [ItemID] int item_id, int stack = -1, byte prefix = 0)
        {
            Terraria.Item.NewItem(new EntitySource_DebugCommand(), player.Handle.position, Vector2.Zero, item_id, stack, false, prefix, true, false);

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            string item_name = Lang.GetItemNameValue(item_id);

            Terraria.Item item = new();
            item.SetDefaults(item_id);

            if (stack is -1)
            {
                stack = item.maxStack;
            }

            Sender.SendFormattedMessage($"Gave {emphasis}{player.Name}{infocol} {emphasis}{stack}{infocol} of {emphasis}{item_name}{infocol}.", infocol);

            player.SendFormattedMessage($"{emphasis}{Sender.LogName}{infocol} gave you {emphasis}{stack}{infocol} of {emphasis}{item_name}{infocol}", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Gave {player.Name} {stack} of {item_name}.");
        }
    }
}
