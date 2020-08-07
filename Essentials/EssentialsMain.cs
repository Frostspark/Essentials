using System;
using System.Runtime.CompilerServices;

namespace Essentials
{
    public class EssentialsMain : Frostspark.API.Plugins.Plugin
    {
        internal static EssentialsMain Instance { get; private set; }

        public EssentialsMain()
        {
            Instance = this;
        }

        public override string Name => "Essentials";

        public override string Author => "quake1337";

        public override void Disable()
        {

        }

        public override void Enable()
        {
            
        }

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public override void Unload()
        {
            throw new NotImplementedException();
        }
    }
}
