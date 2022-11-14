using System.Collections.Concurrent;
using FoodOrderingService.Models;

namespace FoodOrderingService.Services.RestaurantDataService;

public interface IRestaurantDataService
{
    Task<ConcurrentBag<Restaurant>> GetRestaurantData();
    Task Insert(Restaurant restaurantData);
    string GetRestaurantAddressById(int id);
}