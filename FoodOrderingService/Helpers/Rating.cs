using FoodOrderingService.Models;

namespace FoodOrderingService.Helpers;

public static class Rating
{
    static Rating()
    {
        Dictionary = new Dictionary<int, int>();
    }

    private static Dictionary<int, int> Dictionary { get; set; }

    public static Task<double> ComputeRating(OnlineOrder order)
    {
        var now = DateTime.Now;
        var servedTime = Math.Abs(now.Subtract(order.CreateOnTime).TotalSeconds);
        
        if (servedTime < order.MaxWait)
        {
            Dictionary.Add(order.RestaurantId, 5);
        }
        else if (servedTime < order.MaxWait * 1.1)
        {
            Dictionary.Add(order.RestaurantId, 4);
        }
        else if (servedTime < order.MaxWait * 1.2)
        {
            Dictionary.Add(order.RestaurantId, 3);
        }
        else if (servedTime < order.MaxWait * 1.3)
        {
            Dictionary.Add(order.RestaurantId, 2);
        }
        else if (servedTime < order.MaxWait * 1.4)
        {
            Dictionary.Add(order.RestaurantId, 1);
        }

        return Task.FromResult<double>(Dictionary[order.OrderId]);
    }
}