using DiningHall.Models;

namespace DiningHall.Helpers;

public static class RatingHelper
{
    private static readonly List<int> Rating = new();
    
    public static async Task GetRating(Order order)
    {
        var servedTime = Math.Abs(order.UpdatedOnUtc.Subtract(order.CreatedOnUtc).TotalSeconds);

        if (servedTime < order.MaxWait)
        {
            Rating.Add(5);
        }
        else if (servedTime < order.MaxWait * 1.1)
        {
            Rating.Add(4);
        }
        else if (servedTime < order.MaxWait * 1.2)
        {
            Rating.Add(3);
        }
        else if (servedTime < order.MaxWait * 1.3)
        {
            Rating.Add(2);
        }
        else if (servedTime < order.MaxWait * 1.4)
        {
            Rating.Add(1);
        }
        await ConsoleHelper.Print($"Order with Id: {order.Id} was expected in {order.MaxWait} but come in {servedTime}");
        await ConsoleHelper.Print($"Rating for {Rating.Count} is : {Rating.Average()}", ConsoleColor.Magenta);
    }
    
}