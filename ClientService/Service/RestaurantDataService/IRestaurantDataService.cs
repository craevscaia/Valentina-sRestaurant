using Client.Models;

namespace Client.Service.RestaurantDataService;

public interface IRestaurantDataService
{
    
    public Task<IList<RestaurantData>> GetRestaurantData();
    public Task Insert(RestaurantData restaurantData);
    Task<RestaurantData> GetRestaurantDataById(int randomRestaurant);
}