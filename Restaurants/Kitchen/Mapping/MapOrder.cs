using Kitchen.Models;
using Kitchen.Models.Online;

namespace Kitchen.Mapping;

public static class MapOrder
{
    public static Order Map(this OnlineOrder onlineOrder)
    {
        return new Order
        {
            Id = onlineOrder.OrderId,
            Priority = onlineOrder.Priority,
            FoodList = onlineOrder.Foods,
            MaxWait = onlineOrder.MaxWait,
            CreatedOnUtc = onlineOrder.CreateOnTime
        };
    }
}