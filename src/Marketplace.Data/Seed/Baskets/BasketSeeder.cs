using System;
using System.Collections.Generic;
using Marketplace.Baskets;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Marketplace.Data.Seed.Products
{
    public class BasketSeeder
    {
        private readonly IMongoDbContext _dbContext;

        public BasketSeeder(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create()
        {
            var collection = _dbContext.GetCollection<Basket>();

            if (!collection.Find(x => x.CustomerId > 0).Any())
            {
                var basket = Basket.Create(1);

                collection.InsertOne(basket);
            }
        }
    }
}