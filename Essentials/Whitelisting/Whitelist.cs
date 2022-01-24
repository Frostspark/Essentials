using SharedUtils.Configuration;
using SharedUtils.Generic;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Essentials.Whitelisting
{
    public class Whitelist
    {
        private readonly EssentialsPlugin Plugin;

        private readonly HashSet<IPAddress> _allowed = new();

        public bool Enabled { get; set; }

        public Whitelist(EssentialsPlugin plugin) => Plugin = plugin;

        public bool Add(IPAddress addr) => _allowed.Add(addr);

        public bool Remove(IPAddress addr) => _allowed.Remove(addr);

        public bool Contains(IPAddress addr) => _allowed.Contains(addr);

        public ReadOnlyCollection<IPAddress> Allowed => _allowed.ToList().ToReadOnly();

        internal async Task LoadFile()
        {
            string path = Path.Combine(Plugin.DataFolder, "whitelist.json");

            WhitelistFile wf = await ConfigManager.LoadConfigAsync<WhitelistFile>(path);

            if (wf == null)
            {
                Plugin.Log.LogError("Whitelist", "Failed to load whitelist file.");
            }

            Enabled = wf.Status;

            foreach (var ip in wf.Addresses ?? new())
                _allowed.Add(IPAddress.Parse(ip));
        }

        internal async Task SaveFile()
        {
            string path = Path.Combine(Plugin.DataFolder, "whitelist.json");

            WhitelistFile wf = new() { Status = Enabled, Addresses = _allowed.Select(x => x.ToString()).ToList() };

            if (!await ConfigManager.SaveConfigAsync(wf, path))
            {
                Plugin.Log.LogError("Whitelist", "Failed to save whitelist file.");
            }
        }
    }
}
