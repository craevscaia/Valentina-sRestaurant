using FoodOrderingService.Models;

namespace FoodOrderingService.Mapping;

public static class MappingRestaurant
{
    public static Restaurant MapRestaurant(Restaurant restaurant)
    {
        return new Restaurant
        {
            RestaurantId = restaurant.RestaurantId,
            Name = restaurant.Name,
            Address = restaurant.Address,
            MenuItems = restaurant.MenuItems,
            Menu = restaurant.Menu,
            Raiting = restaurant.Raiting
        };
    }
}