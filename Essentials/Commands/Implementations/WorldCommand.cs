using Essentials.Enums;

using Frostspark.API.Enums;
using Frostspark.API.Worlds.Time;
using Frostspark.Server;
using Frostspark.Server.Commands.Attributes;
using Frostspark.Server.Entities;

using SharedUtils.Commands.Attributes;
using SharedUtils.Commands.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Main = Terraria.Main;

namespace Essentials.Commands.Implementations
{
    [CommandName("world")]
    [CommandDescription("Manages world information")]
    [CommandPermission("essentials.commands.world")]
    public class WorldCommand : CommandWrapper<CommandSender>
    {
        private static Server Server => EssentialsPlugin.Server;

        [SubCommand("difficulty", "diff", "mode")]
        [CommandPermission("essentials.commands.world.difficulty")]
        public class WorldDifficultyCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void Get()
            {
                var colors = Server.Colors;
                var difficulty = Server.World.Difficulty;

                Sender.SendFormattedMessage($"Current world difficulty: {colors.TargetEmphasis}{difficulty}{colors.Info}.", colors.Info);
            }

            [CommandCallback]
            public void Set(WorldDifficulty new_difficulty)
            {
                var colors = Server.Colors;
                var old_diff = Server.World.Difficulty;

                Server.World.Difficulty = new_difficulty;

                Sender.SendFormattedMessage($"Changed world difficulty from {colors.TargetEmphasis}{old_diff}{colors.Success} to {colors.TargetEmphasis}{new_difficulty}{colors.Success}.", colors.Success);

                Server.Commands.LogCommandActivity(Sender, $"Changed world difficulty from {old_diff} to {new_difficulty}.");

            }
        }

        [SubCommand("name", "rename")]
        [CommandPermission("essentials.commands.world.name")]
        public class WorldNameCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void Get()
            {
                var colors = Server.Colors;
                var name = Server.World.Name;

                Sender.SendFormattedMessage($"Current world name: {colors.TargetEmphasis}{name}{colors.Info}", colors.Info);
            }

            [CommandCallback]
            public void Set(string name)
            {
                var colors = Server.Colors;
                var old_name = Server.World.Name;

                Server.World.Name = name;

                Sender.SendFormattedMessage($"Changed world name from {colors.TargetEmphasis}{old_name}{colors.Success} to {colors.TargetEmphasis}{name}{colors.Success}.", colors.Success);
            }
        }

        [SubCommand("hardmode")]
        [CommandPermission("essentials.commands.world.hardmode")]
        public class WorldHardmodeCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void Get()
            {
                var colors = Server.Colors;

                var value = Server.World.Hardmode;

                Sender.SendFormattedMessage($"Hardmode is {(value ? (FormattableString)$"{colors.Success}enabled" : (FormattableString)$"{colors.Error}disabled")}{colors.Info}.", colors.Info);
            }

            [CommandCallback]
            public void Set(bool state)
            {
                var colors = Server.Colors;

                Server.World.Hardmode = state;

                Sender.SendFormattedMessage($"Hardmode has been {(state ? (FormattableString)$"{colors.Success}enabled" : (FormattableString)$"{colors.Error}disabled")}{colors.Info}.", colors.Info);
            }
        }

        [SubCommand("time")]
        [CommandPermission("essentials.commands.world.time")]
        public class WorldTimeCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            [SubCommand("get")]
            public void CheckTime()
            {
                var curr_time = Server.World.Time;

                Sender.SendInfoMessage($"The current world time is {curr_time.Hour:00}:{curr_time.Minute:00}.");
            }

            [CommandCallback]
            [SubCommand("set")]
            public void Set(WorldTime time)
            {
                Server.World.Time = time;

                Sender.SendInfoMessage($"The world time was set to {time.Hour:00}:{time.Minute:00}.");
            }
        }

        [SubCommand("weather")]
        [CommandPermission("essentials.commands.world.weather")]
        public class WorldWeatherCommand : CommandWrapper<CommandSender>
        {
            /// * GAMEINFO:: 1.4.3.2
            /// * Weather types:
            /// * - LightRain = Strength > 0, < 0.2
            /// * - Rain = Strength >= 0.2, < 0.6
            /// * - Heavy Rain = Strength >= 0.6
            /// * - Storming = Main.IsItStorming (high wind in either direction + clouds over _minRain)

            private readonly Dictionary<WeatherType, Action> WeatherActors = new();

            public WorldWeatherCommand()
            {
                WeatherActors.Add(WeatherType.Sunny, SetSunny);
                WeatherActors.Add(WeatherType.LightRain, SetLightRain);
                WeatherActors.Add(WeatherType.Rain, SetRain);
                WeatherActors.Add(WeatherType.HeavyRain, SetHeavyRain);
                WeatherActors.Add(WeatherType.Storm, SetStorm);
                WeatherActors.Add(WeatherType.SlimeRain, SetSlimeRain);
            }

            [CommandCallback]
            public void SetWeatherType(WeatherType type)
            {
                if (WeatherActors.TryGetValue(type, out var act))
                {
                    act();

                    Terraria.NetMessage.SendData(7);

                    var col = Server.Instance.Colors;

                    Sender.SendFormattedMessage($"The world weather has been updated to {col.TargetEmphasis}{type}{col.Info}.", col.Info);

                    Server.Instance.Commands.LogCommandActivity(Sender, $"Changed world weather to {type}");
                }
            }

            private void SetSunny()
            {
                ClearEvents();

                Main.rainTime = 0;
                Main.raining = false;
                Main.maxRaining = 0.0f;
                Main.numClouds = 0;
                Main.cloudBGActive = 0.0f;
            }

            private void SetRain()
            {
                ClearEvents();

                Main.rainTime = 3600;
                Main.raining = true;
                Main.maxRaining = 0.5f;
                Main.numClouds = 100;
                Main.cloudBGActive = 0.4f;
            }

            private void SetHeavyRain()
            {
                ClearEvents();

                Main.rainTime = 3600;
                Main.raining = true;
                Main.maxRaining = 1.0f;
                Main.numClouds = 200;
                Main.cloudBGActive = 1.3f;
            }

            private void SetLightRain()
            {
                ClearEvents();

                Main.rainTime = 3600;
                Main.raining = true;
                Main.maxRaining = 0.19f;
                Main.numClouds = 50;
                Main.cloudBGActive = 0.4f;
            }

            private void SetStorm()
            {
                ClearEvents();

                Main.cloudAlpha = Main._maxRain * 1.15f;

                if (Main.windSpeedTarget < 0)
                    Main.windSpeedTarget = -(Main._maxWind * 1.15f);
                else
                    Main.windSpeedTarget = (Main._maxWind * 1.15f);

                Main.rainTime = 3600;
                Main.raining = true;
                Main.maxRaining = 0.5f;
                Main.numClouds = 0;
                Main.cloudBGActive = 1f;
            }

            private void SetSlimeRain()
            {
                ClearEvents();

                Main.StartSlimeRain(false);
            }

            private static void ClearEvents()
            {
                if (Main.slimeRain)
                    Main.StopSlimeRain(false);

                if (Main.raining)
                    Main.StopRain();

                if (Main.windSpeedTarget >= Main._maxWind)
                    Main.windSpeedTarget = Main._maxWind * 0.5f;

                if (Main.windSpeedTarget <= -Main._maxWind)
                    Main.windSpeedTarget = -(Main._maxWind * 0.5f);
            }
        }

        [SubCommand("wind")]
        [CommandPermission("essentials.commands.world.wind")]
        public class WorldWindCommand : CommandWrapper<CommandSender>
        {
            [CommandCallback]
            public void Set(float value)
            {
                Main.windSpeedTarget = value;

                var col = Server.Instance.Colors;

                Sender.SendFormattedMessage($"The wind strength has been updated to {col.TargetEmphasis}{value}{col.Info}.", col.Info);

                Server.Instance.Commands.LogCommandActivity(Sender, $"Changed wind strength to {value}");

                Terraria.NetMessage.SendData(7);
            }
        }
        [SubCommand("spawning", "spawns")]
        [CommandDescription("Modifies the server-wide NPC spawning settings.")]
        [CommandPermission("essentials.commands.world.spawning")]
        public class WorldSpawningCommand : CommandWrapper<CommandSender>
        {
            [SubCommand("rate")]
            [CommandPermission("essentials.commands.world.spawning.rate")]
            public class SpawningRateCommand : CommandWrapper<CommandSender>
            {
                [CommandCallback]
                public void GetRate()
                {
                    var infocol = EssentialsPlugin.Server.Colors.Info;
                    var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                    Sender.SendFormattedMessage($"The current spawnrate setting is {emph}{Terraria.NPC.defaultSpawnRate}{infocol}.", infocol);
                }

                [CommandCallback]
                public void SetRate(int rate)
                {
                    var infocol = EssentialsPlugin.Server.Colors.Info;
                    var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                    Terraria.NPC.defaultSpawnRate = rate;

                    Sender.SendFormattedMessage($"The spawnrate setting has been updated to {emph}{rate}{infocol}.", infocol);

                    EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Updated the spawnrate to {rate}.");
                }
            }

            [SubCommand("max")]
            [CommandPermission("essentials.commands.world.spawning.max")]
            public class SpawningMaxCommand : CommandWrapper<CommandSender>
            {
                [CommandCallback]
                public void GetRate()
                {
                    var infocol = EssentialsPlugin.Server.Colors.Info;
                    var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                    Sender.SendFormattedMessage($"The current max-spawns setting is {emph}{Terraria.NPC.defaultMaxSpawns}{infocol}.", infocol);
                }

                [CommandCallback]
                public void SetRate(int rate)
                {
                    var infocol = EssentialsPlugin.Server.Colors.Info;
                    var emph = EssentialsPlugin.Server.Colors.TargetEmphasis;

                    Terraria.NPC.defaultMaxSpawns = rate;

                    Sender.SendFormattedMessage($"The max-spawns setting has been updated to {emph}{rate}{infocol}.", infocol);

                    EssentialsPlugin.Server.Commands.LogCommandActivity(Sender, $"Updated max-spawns to {rate}.");
                }
            }
        }
    }
}
