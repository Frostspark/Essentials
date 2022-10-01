using Essentials.Utilities;

using Frostspark.API.Worlds.Time;
using Frostspark.Server.Commands.Assertions;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Commands.Parsers;
using Frostspark.Server.Entities;
using Frostspark.Server.Utilities.Extensions;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;

using Terraria;
using Terraria.DataStructures;
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
        public void Spawn([NPCID(aliases: true)] int npc_id, int amount = 1, bool on_self = false)
        {
            if (!EntityAssertions.Assert_SenderPlayer(Sender, out var ply))
                return;

            amount = Math.Min(amount, Main.maxNPCs);

            if (PreSpawnActions.TryGetValue(npc_id, out var pre_deleg))
                pre_deleg();

            if (SpecialBindings.TryGetValue(npc_id, out var deleg))
            {
                deleg(ply, amount, on_self);
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    if (on_self)
                    {
                        Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)ply.Position.X, (int)ply.Position.Y, npc_id);
                    }
                    else
                    {
                        (int x, int y) = TileUtils.GetRandomClearTile((int)ply.Position.X / 16, (int)ply.Position.Y / 16, 100, 50);
                        Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), x * 16, y * 16, npc_id);
                    }
                }

                var infocol = EssentialsPlugin.Server.Colors.Info;
                var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

                var npc_name = Lang.GetNPCNameValue(npc_id);

                ply.SendFormattedMessage($"Spawned {emphasis}{amount}{infocol} of {emphasis}{npc_name}{infocol}.", infocol);

                EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Spawned {amount} of {npc_name}.");
            }
        }

        private static void SetTimeNight()
        {
            EssentialsPlugin.Server.World.Time = new WorldTime(19, 30);
        }

        private static void SpawnTwins(Frostspark.Server.Entities.Player player, int amount, bool on_self = false)
        {
            for (int i = 0; i < amount; i++)
            {
                if (on_self)
                {
                    Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)player.Position.X, (int)player.Position.Y, 125);
                    Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)player.Position.X, (int)player.Position.Y, 126);
                }
                else
                {
                    int plyx = (int)player.Position.X / 16;
                    int plyy = (int)player.Position.Y / 16;

                    (int x, int y) = TileUtils.GetRandomClearTile(plyx, plyy, 100, 50);
                    Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), x * 16, y * 16, 125);
                    (x, y) = TileUtils.GetRandomClearTile(plyx, plyy, 100, 50);
                    Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), x * 16, y * 16, 126);
                }
            }

            var infocol = EssentialsPlugin.Server.Colors.Info;
            var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

            player.SendFormattedMessage($"Spawned {emphasis}{amount}{infocol} pair(s) of {emphasis}Twins{infocol}.", infocol);

            EssentialsPlugin.Server.Commands.LogCommandActivity(player, $"Spawned {amount} pairs of Twins.");
        }

        private static void SpawnMechdusa(Frostspark.Server.Entities.Player player, int amount, bool on_self = false)
        {
            if (Terraria.NPC.IsMechQueenUp)
            {
                player.SendErrorMessage($"Mech Queen cannot be currently spawned as one is already active.");
            }
            else
            {
                int skeletron = -1;

                if (on_self)
                {
                    skeletron = Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)player.Position.X, (int)player.Position.Y, NPCID.SkeletronPrime);
                }
                else
                {
                    int plyx = (int)player.Position.X / 16;
                    int plyy = (int)player.Position.Y / 16;

                    (int x, int y) = TileUtils.GetRandomClearTile(plyx, plyy, 100, 50);
                    skeletron = Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), x * 16, y * 16, NPCID.SkeletronPrime, 0, 0, 0, 0, 99999f);
                }

                if (skeletron is 200)
                {
                    player.SendErrorMessage($"Mechdusa spawn aborted: NPC cap reached.");
                }
                else
                {
                    Main.npc[skeletron].ai[3] = skeletron;

                    var skeletron_pos = Main.npc[skeletron].Center;

                    int retinazer = Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)skeletron_pos.X, (int)skeletron_pos.Y, NPCID.Retinazer);
                    int spazmatism = Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)skeletron_pos.X, (int)skeletron_pos.Y, NPCID.Spazmatism);
                    int destroyer = Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)skeletron_pos.X, (int)skeletron_pos.Y, NPCID.TheDestroyer);

                    if (destroyer is not 200)
                    {
                        int probe1 = Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)skeletron_pos.X, (int)skeletron_pos.Y, NPCID.Probe, 0, 0, 0, destroyer, -1f);
                        int probe2 = Terraria.NPC.NewNPC(new EntitySource_DebugCommand(), (int)skeletron_pos.X, (int)skeletron_pos.Y, NPCID.Probe, 0, 0, 0, destroyer, 1f);
                    }

                    var infocol = EssentialsPlugin.Server.Colors.Info;
                    var emphasis = EssentialsPlugin.Server.Colors.TargetEmphasis;

                    player.SendFormattedMessage($"Spawned {emphasis}Mechdusa{infocol}.", infocol);

                    EssentialsPlugin.Server.Commands.LogCommandActivity(player, $"Spawned Mechdusa.");
                }
            }
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

                EssentialsPlugin.Server.Commands.LogCommandActivity(player, $"Spawned Wall of Flesh.");
            }
        }
    }
}
