using Marketplace.Data.Seed.Products;

namespace Marketplace.Data.Seed
{
    public static class SeedHelper
    {
        public static void Seed(IMongoDbContext dbContext)
        {
            new BasketSeeder(dbContext).Create();
        }
    }
}