using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.BasketEntites;
using Microsoft.VisualBasic;
using StackExchange.Redis;

namespace Presistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        // Redis Connection or any other database connection would be injected here
        readonly IDatabase redis_Database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            redis_Database = connection.GetDatabase();
        }

        public async Task<Basket?> CreateorUpdateBasketAsync(Basket basket , TimeSpan time)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreated = await redis_Database.StringSetAsync(basket.Id, JsonBasket, time);
            if (IsCreated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Basket could not be created or updated.");
            }
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await redis_Database.KeyDeleteAsync(id);
        }

        public async Task<Basket?> GetBasketAsync(string id)
        {
            var JsonBasket = await redis_Database.StringGetAsync(id);
            if (JsonBasket.IsNullOrEmpty)
            {
                return null;
            }
            var basket = JsonSerializer.Deserialize<Basket>(JsonBasket);
            return basket!;
        }
    }
}
