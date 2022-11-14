using System.Collections.Concurrent;
using FoodOrderingService.Models;

namespace FoodOrderingService.Repositories.RestaurantDataRepository;

public interface IRestaurantDataRepository
{
    Task<ConcurrentBag<Restaurant>> GetRestaurantData();
    Task Insert(Restaurant restaurantData);
    string GetRestaurantAddressById(int id);
}