using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAync(CustomerBasket Basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(Basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(Basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string Id) => await _database.KeyDeleteAsync(Id);

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var Basket = await _database.StringGetAsync(Key);
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);

        }
    }
}
