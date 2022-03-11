using Frostspark.Server.Commands.Assertions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Commands.Parsers;
using Frostspark.Server.Entities;

using Microsoft.Xna.Framework;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Attributes.Overrides;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

using Terraria;
using Terraria.DataStructures;

namespace Essentials.Commands.Implementations
{
    [CommandName("item", "i")]
    [CommandDescription("Gives an item to the issuer")]
    [CommandPermission("essentials.commands.item")]
    public class ItemCommand : CommandWrapper<CommandSender>
    {
        [CommandCallback]
        public void SpawnItem([ItemID] int item_id, int stack = 1, byte prefix = 0)
        {
            if (!EntityAssertions.Assert_SenderPlayer(Sender, out var ply))
                return;

            Terraria.Item.NewItem(new EntitySource_DebugCommand(), ply.Handle.position, Vector2.Zero, item_id, stack, false, prefix, true, false);

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            string item_name = Lang.GetItemNameValue(item_id);

            Sender.SendFormattedMessage($"Spawned {emphasis}{stack}{infocol} of {emphasis}{item_name}{infocol}.", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Gave self {stack} of {item_name}.");
        }

        [CommandCallback]
        public void SpawnItem([ItemID(multiword: true)] int item_id)
        {
            if (!EntityAssertions.Assert_SenderPlayer(Sender, out var ply))
                return;

            Terraria.Item item = new();
            item.SetDefaults(item_id);

            int stack = item.maxStack;

            Terraria.Item.NewItem(new EntitySource_DebugCommand(), ply.Handle.position, Vector2.Zero, item_id, stack, false, 0, true, false);

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            string item_name = Lang.GetItemNameValue(item_id);

            Sender.SendFormattedMessage($"Spawned {emphasis}{stack}{infocol} of {emphasis}{item_name}{infocol}.", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Gave self {stack} of {item_name}.");
        }
    }
}
