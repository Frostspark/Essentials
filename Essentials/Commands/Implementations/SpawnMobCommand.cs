using Essentials.Utilities;

using Frostspark.API.Utilities;
using Frostspark.API.Worlds.Time;
using Frostspark.Server;
using Frostspark.Server.Commands.Assertions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Commands.Parsers;
using Frostspark.Server.Entities;
using Frostspark.Server.Utilities;
using Frostspark.Server.Utilities.Extensions;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Text;

using Terraria;
using Terraria.ID;

namespace Essentials.Commands.Implementations
{
    [CommandName("spawnmob", "sm")]
    [CommandDescription("Spawns NPCs")]
    [CommandPermission("essentials.commands.spawnmob")]
    public class SpawnMobCommand : CommandWrapper<CommandSender>
    {
        private static Dictionary<int, Action<Frostspark.Server.Entities.Player, int, bool>> SpecialBindings = new Dictionary<int, Action<Frostspark.Server.Entities.Player, int, bool>>()
        {
            { NPCID.WallofFlesh, SpawnWOF },
            { NPCID.Retinazer, SpawnTwins },
            { NPCID.Spazmatism, SpawnTwins }
        };

        private static Dictionary<int, Action> PreSpawnActions = new Dictionary<int, Action>()
        {
            { NPCID.EyeofCthulhu, SetTimeNight },
            { NPCID.SkeletronHead, SetTimeNight },
            { NPCID.Retinazer, SetTimeNight },
            { NPCID.Spazmatism, SetTimeNight },
            { NPCID.SkeletronPrime, SetTimeNight },
            { NPCID.TheDestroyer, SetTimeNight },
        };

        [CommandCallback]
        public void Spawn( [NPCID(aliases: true)] int npc_id, int amount = 1, bool on_self = false)
        {
            if (!EntityAssertions.Assert_SenderPlayer(Sender, out var ply))
                return;

            amount = Math.Min(amount, Main.maxNPCs);

            if (PreSpawnActions.TryGetValue(npc_id, out var pre_deleg))
                pre_deleg();

            if(SpecialBindings.TryGetValue(npc_id, out var deleg))
            {
                deleg(ply, amount, on_self);
            }
            else
            {
                for(int i = 0; i < amount; i++)
                {
                    if(on_self)
                    {
                        Terraria.NPC.NewNPC((int)ply.Position.X, (int)ply.Position.Y, npc_id);
                    }
                    else
                    {
                        (int x, int y) = TileUtils.GetRandomClearTile((int)ply.Position.X / 16, (int)ply.Position.Y / 16, 100, 50);
                        Terraria.NPC.NewNPC(x * 16, y * 16, npc_id);
                    }
                }

                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

                ply.SendFormattedMessage($"Spawned {emphasis}{amount}{infocol} of {emphasis}{Lang.GetNPCNameValue(npc_id)}{infocol}.", infocol);
            }
        }

        private static void SetTimeNight()
        {
            EssentialsPlugin.Server.World.Time = new WorldTime(19, 30);
        }

        private static void SpawnTwins(Frostspark.Server.Entities.Player player, int amount, bool on_self = false)
        {
            for(int i = 0; i < amount; i++)
            {
                if(on_self)
                {
                    Terraria.NPC.NewNPC((int)player.Position.X, (int)player.Position.Y, 125);
                    Terraria.NPC.NewNPC((int)player.Position.X, (int)player.Position.Y, 126);
                }
                else
                {
                    int plyx = (int)player.Position.X;
                    int plyy = (int)player.Position.Y;

                    (int x, int y) = TileUtils.GetRandomClearTile(plyx, plyy, 100, 50);
                    Terraria.NPC.NewNPC(x, y, 125);
                    (x, y) = TileUtils.GetRandomClearTile(plyx, plyy, 100, 50);
                    Terraria.NPC.NewNPC(x, y, 126);
                }
            }

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            player.SendFormattedMessage($"Spawned {emphasis}{amount}{infocol} pair(s) of {emphasis}Twins{infocol}.", infocol);
        }

        private static void SpawnWOF(Frostspark.Server.Entities.Player player, int amount, bool on_self = false)
        {
            if (Main.wofNPCIndex != -1 || player.Position.Y / 16 < (Main.maxTilesY - 205))
            {
                player.SendErrorMessage("Wall of Flesh cannot be spawned at your current location.");
            }
            else
            {
                Terraria.NPC.SpawnWOF(XNAConversions.FSToXNA(player.Position));

                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                player.SendFormattedMessage($"Spawned {emph}Wall of Flesh{infocol}.", infocol);
            }
        }
    }
}
