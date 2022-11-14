using Client.Models;
using Client.Repositories.RestaurantDataRepository;

namespace Client.Service.RestaurantDataService;

public class RestaurantDataService  : IRestaurantDataService
{
    private readonly IRestaurantDataRepository _restaurantDataRepository;

    public RestaurantDataService(IRestaurantDataRepository restaurantDataRepository)
    {
        _restaurantDataRepository = restaurantDataRepository;
    }

    public Task<IList<RestaurantData>> GetRestaurantData()
    {
        return _restaurantDataRepository.GetRestaurantData()!;
    }

    public Task Insert(RestaurantData restaurantData)
    {
        return _restaurantDataRepository.Insert(restaurantData);
    }

    public Task<RestaurantData> GetRestaurantDataById(int randomRestaurant)
    {
        return _restaurantDataRepository.GetRestaurantDataById(randomRestaurant);
    }
}