using DiningHall.Models;
using DiningHall.Models.OnlineOrders;
using DiningHall.SettingsFolder;

namespace DiningHall.Mapping;

public static class Mapping
{
    public static Order MapOrder(this OnlineOrder onlineOrder)
    {
        return new Order
        {
            Id = onlineOrder.OrderId,
            Priority = onlineOrder.Priority,
            ClientId = onlineOrder.ClientId,
            FoodList = onlineOrder.Foods,
            MaxWait = onlineOrder.MaxWait,
            CreatedOnUtc = onlineOrder.CreateOnTime,
        };
    }
    public static OnlineOrder MapOnlineOrder(this Order onlineOrder)
    {
        return new OnlineOrder
        {
            ClientId = onlineOrder.ClientId,
            Priority = onlineOrder.Priority,
            Foods = onlineOrder.FoodList,
            MaxWait = onlineOrder.MaxWait,
            OrderId = onlineOrder.Id,
            RestaurantId = Settings.RestaurantId,
            CreateOnTime = onlineOrder.CreatedOnUtc,
        };
    }
}