using System.Collections.Concurrent;
using FoodOrderingService.Models;
using FoodOrderingService.Repositories.RestaurantDataRepository;

namespace FoodOrderingService.Services.RestaurantDataService;

public class RestaurantDataService : IRestaurantDataService
{
    private readonly IRestaurantDataRepository _restaurantDataRepository;

    public RestaurantDataService(IRestaurantDataRepository restaurantDataRepository)
    {
        _restaurantDataRepository = restaurantDataRepository;
    }
    public Task<ConcurrentBag<Restaurant>> GetRestaurantData()
    {
        return _restaurantDataRepository.GetRestaurantData();
    }

    public Task Insert(Restaurant restaurantData)
    {
        return _restaurantDataRepository.Insert(restaurantData);
    }

    public string GetRestaurantAddressById(int id)
    {
        return _restaurantDataRepository.GetRestaurantAddressById(id);
    }
}