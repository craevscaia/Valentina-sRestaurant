using Client.Models;

namespace Client.Repositories.RestaurantDataRepository;

public interface IRestaurantDataRepository
{
    Task<IList<RestaurantData>?> GetRestaurantData();
    Task Insert(RestaurantData restaurantData);
    Task<RestaurantData> GetRestaurantDataById(int randomRestaurant);
    void SetRestaurantData(IList<RestaurantData> restaurantData);
}