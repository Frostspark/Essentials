using Essentials.Enums;

using SharedUtils.Commands.Attributes.Parsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Commands.Parsers
{
    internal class WeatherTypeParser
    {
        [TypeParser(typeof(WeatherType))]
        public static WeatherType ParseWeatherType(string s)
        {
            return s.ToLower() switch
            {
                "rain" or "raining" => WeatherType.Rain,
                "lightrain" or "lightraining" or "lightlyraining" => WeatherType.LightRain,
                "heavyrain" or "heavyraining" or "heavilyraining" => WeatherType.HeavyRain,
                "storm" or "storming" => WeatherType.Storm,
                "clear" or "sunny" or "sun" or "clean" => WeatherType.Sunny,
                "slimerain" or "sliming" or "slime" => WeatherType.SlimeRain,
                _ => throw new FormatException("Unknown weather type. Known types: rain, lightrain, heavyrain, storm, sunny, slimerain")
            };
        }
    }
}
