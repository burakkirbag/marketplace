using System.Runtime.CompilerServices;

namespace Marketplace.Engine
{
    public static class EngineContext
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
            => Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new MarketplaceEngine());

        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                    Create();

                return Singleton<IEngine>.Instance;
            }
        }
    }
}