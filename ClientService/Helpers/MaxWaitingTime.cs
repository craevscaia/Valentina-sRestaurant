using Client.Models;

namespace Client.Helpers;

public static class MaxWaitingTime
{
    public static Task<int> CalculateMaxWaitingTine(IEnumerable<Food> randomFoodList)
    {
        var maxFood = randomFoodList.MaxBy(food => food.PreparationTime);
        if (maxFood == null) return Task.FromResult(0);
        
        var time = maxFood.PreparationTime * 1.8;
        return Task.FromResult((int) Math.Ceiling(time));
    }
}