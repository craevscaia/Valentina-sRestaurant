using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.SettingsFolder;

namespace DiningHall.Services.FoodService;

public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public Task<List<int>> GenerateOrderFood()
    {
        var size = RandomGenerator.NumberGenerator(Settings.FoodListSize);
        var listOfFood = new List<int>();

        for (var id = 0; id < size; id++)
        {
            var randomNumber = RandomGenerator.NumberGenerator(13);
            while (listOfFood.Contains(randomNumber))
            {
                randomNumber = RandomGenerator.NumberGenerator(13);
            }

            listOfFood.Add(randomNumber);
        }

        return Task.FromResult(listOfFood);
    }

    public Task<IList<Food>> GetAll()
    {
        return _foodRepository.GetAll();
    }

    public Task<Food?> GetById(int id)
    {
        return _foodRepository.GetById(id);
    }

    public Task GenerateMenu()
    {
        return _foodRepository.GenerateMenu();
    }
}