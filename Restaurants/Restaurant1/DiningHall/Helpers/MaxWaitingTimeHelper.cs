using DiningHall.Services.FoodService;

namespace DiningHall.Helpers;

public static class MaxWaitingTimeHelper
{
    public static int CalculateMaximWaitingTime(this IEnumerable<int> foodList, IFoodService repository)
    {
        var maxWaitingTime = 0;

        foreach (var foodId in foodList)
        {
            var food = repository.GetById(foodId);
            if (food.Result != null)
            {
                maxWaitingTime += food.Result.PreparationTime;
            }
        }

        return (int) Math.Ceiling(maxWaitingTime * 1.3);
    }
}