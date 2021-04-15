using Marketplace.Data.Seed;
using Marketplace.Engine;

namespace Marketplace.Data
{
    public class DataStartupTask : IStartupTask
    {
        public void Execute()
        {
            var dbContext = EngineContext.Current.Resolve<IMongoDbContext>();

            SeedHelper.Seed(dbContext);
        }

        public int Order => 4;
    }
}