namespace DiningHall.SettingsFolder;

public static class Settings
{
    public const int NrOfTables = 6;
    public const int NrOfWaiters = 3;
    public const int RestaurantId = 1;

    public const int FoodListSize = 10;

    // public const string KitchenUrl = "http://host.docker.internal:7284/kitchen";
    public const string KitchenUrl = "https://localhost:7284/kitchen";
    public const string KitchenUrlResponse = "https://localhost:7284/response";
    
    public const string FoodOrderingServiceRegisterUrl = "https://localhost:7077/register";
    public const string FoodOrderingServiceResponseUrl = "https://localhost:7077/response";
    public const string FoodOrderingServiceReceiveOrderUrl = "https://localhost:7077/orderready";


    public const string Menu = "U:\an3\\PR\\Valentina'sRestaurant\\Valentina'sRestaurant\\Restaurants\\Restaurant1\\DiningHall\\JSON\\Menu.json";
    public const string RestaurantData = "U:\\an3\\PR\\Valentina'sRestaurant\\Valentina'sRestaurant\\Restaurants\\Restaurant1\\DiningHall\\JSON\\Restaurant.json";
}