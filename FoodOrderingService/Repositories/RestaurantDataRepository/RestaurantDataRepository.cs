using System.Collections.Concurrent;
using FoodOrderingService.Models;

namespace FoodOrderingService.Repositories.RestaurantDataRepository;

public class RestaurantDataRepository : IRestaurantDataRepository
{
    private ConcurrentBag<Restaurant> RestaurantData { get; set; }

    public RestaurantDataRepository()
    {
        RestaurantData = new ConcurrentBag<Restaurant>();
    }

    public Task<ConcurrentBag<Restaurant>>  GetRestaurantData()
    {
        return Task.FromResult(RestaurantData);
    }

    public Task Insert(Restaurant restaurantData)
    {
        RestaurantData.Add(restaurantData);
        return Task.CompletedTask;
    }
    
    public string GetRestaurantAddressById(int id)
    {
        return RestaurantData.First(restaurant => restaurant.RestaurantId.Equals(id)).Address;
    }
}