using SharedUtils.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Essentials.Whitelisting
{
    internal class WhitelistFile : Config
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("addresses")]
        public List<string> Addresses { get; set; }

        public override void SetDefaults()
        {
            Status = false;
            Addresses = new();
        }
    }
}
