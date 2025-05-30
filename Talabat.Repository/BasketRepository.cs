﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Core;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer rides)
        {
            _database = rides.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
           var Basket = await _database.StringGetAsync(BasketId); 
            return Basket.IsNull ? null :JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize<CustomerBasket>(Basket);
            var CreatedBasket = await _database.StringSetAsync(Basket.Id, JsonBasket,TimeSpan.FromDays(1));
            return !CreatedBasket ? null : await GetBasketAsync(Basket.Id);
        }
    }
}
