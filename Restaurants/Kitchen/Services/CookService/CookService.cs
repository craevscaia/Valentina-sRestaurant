using System.Collections.Concurrent;
using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Repositories.CookRepository;
using Kitchen.Services.FoodService;

namespace Kitchen.Services.CookService;

public class CookService : ICookService
{
    private readonly ICookRepository _cookRepository;
    private readonly IFoodService _foodService;
    private readonly List<Task> _tasks;

    public CookService(ICookRepository cookRepository, IFoodService foodService)
    {
        _cookRepository = cookRepository;
        _foodService = foodService;
        _tasks = new List<Task>();
    }

    public Task GenerateCooks()
    {
        return _cookRepository.GenerateCooks();
    }

    public Task<ConcurrentBag<Cook>> GetAll()
    {
        return _cookRepository.GetAll();
    }

    public Task<Cook> GetById(int id)
    {
        return _cookRepository.GetById(id);
    }

    public Task<Cook?> GetFreeCook()
    {
        return _cookRepository.GetFreeCook();
    }

    public Task<Cook> GetCookerByRank(int rank)
    {
        return _cookRepository.GetCookerByRank(rank);
    }

    public async Task AddFoodToCookerList(Order order, IEnumerable<Food> foodList, Dictionary<int, List<Task>> tasks)
    {
        var foods = new List<Food>(foodList);
        foreach (var food in foods.ToList())
        {
            switch (food.Complexity)
            {
                case 3:
                {
                    await PrepareFoodForCooker(order, food, tasks, foods);
                    break;
                }

                case 2:
                {
                    await PrepareFoodForCooker(order, food, tasks, foods);
                    break;
                }
                case 1:
                {
                    await PrepareFoodForCooker(order, food, tasks, foods);
                    break;
                }
            }
        }

        if (foods.Any())
        {
            await SleepFunctionCall(foods);
            await AddFoodToCookerList(order, foods, tasks);
        }

        await Task.WhenAll(tasks[order.Id]);
    }

    //This cooker is done special for simple orders who can be fast done
    public async Task CallSpecialCooker(Order order, IEnumerable<Food> foodList, Dictionary<int, List<Task>> tasks)
    {
        var foods = new List<Food>(foodList);
        var cooker = await _cookRepository.GetSpecialCooker(2, 2);
        if (cooker != null)
        {
            foreach (var food in foods.ToList())
            {
                if (cooker.CookingList.Count < cooker.Proficiency)
                {
                    await AdjustCookerCookingList(order, cooker, food, foods, tasks);
                }
                else
                {
                    break;
                }
            }
        }

        if (foods.Any())
        {
            await SleepFunctionCall(foods);
        }

        await Task.WhenAll(tasks[order.Id]);
        order.UpdatedOnUtc = DateTime.Now;
        Console.WriteLine("All the food from this order was cooked");
    }

    private async Task SleepFunctionCall(IEnumerable<Food> foods)
    {
        var foodThatPreparesQuickest = await _foodService.GetFoodThatPreparesQuickest(foods);
        if (foodThatPreparesQuickest != null)
        {
            await SleepGenerator.Delay(2);
        }
    }

    private async Task PrepareFoodForCooker(Order order, Food food, Dictionary<int, List<Task>> tasks,
        ICollection<Food> foods)
    {
        var cooker = await _cookRepository.GetCookerByRank(food.Complexity);

        if (cooker.CookingList.Count < cooker.Proficiency)
        {
            await AdjustCookerCookingList(order, cooker, food, foods, tasks);
        }
        else
        {
            //There is no one above the main chef, so there is no one who could help him
            if (food.Complexity != 3)
            {
                await CallCookForHelp(order, food, tasks, foods);
            }
        }
    }

    private async Task CallCookForHelp(Order order, Food food, Dictionary<int, List<Task>> tasks,
        ICollection<Food> foods)
    {
        var helperCooker = await _cookRepository.GetCookerByRank(food.Complexity + 1);
        if (helperCooker.CookingList.Count < helperCooker.Proficiency)
        {
            await ConsoleHelper.Print($"A helping {helperCooker.Name} come to help", ConsoleColor.Yellow);
            await AdjustCookerCookingList(order, helperCooker, food, foods, tasks);
        }
    }

    private async Task AdjustCookerCookingList(Order order, Cook cooker, Food food, ICollection<Food> foods,
        IDictionary<int, List<Task>> tasksDictionary)
    {
        cooker.CookingList.Add(food);
        await ConsoleHelper.Print($"I am {cooker.Name} and I will cook {food.Name}", ConsoleColor.DarkBlue);
        var task = Task.Run(() => CookFood(order, cooker, food));
        if (tasksDictionary.ContainsKey(order.Id))
        {
            _tasks.Add(task);
            tasksDictionary[order.Id] = _tasks;
        }
        else
        {
            _tasks.Add(task);
            tasksDictionary.Add(order.Id, _tasks);
        }

        foods.Remove(food);
    }

    private async Task CookFood(Order order, Cook cooker, Food food)
    {
        if (food.CookingApparatus != null)
        {
            if (food.CookingApparatus.IsBusy)
            {
                await SleepGenerator.Delay(1);
            }
            else
            {
                await CookFoodInCookingApparatus(food, order, cooker);
            }
        }
        else
        {
            await CookFoodWithoutApparatus(food, order, cooker);
        }
    }

    private async Task CookFoodWithoutApparatus(Food food, Order order, Cook cooker)
    {
        await SleepGenerator.Delay(food.PreparationTime);
        cooker.CookingList.Remove(food);
        await ConsoleHelper.Print($"I am {cooker.Name} and cooked {food.Name} from order {order.Id}",
            ConsoleColor.Green);
    }

    private async Task CookFoodInCookingApparatus(Food food, Order order, Cook cooker)
    {
        if (food.CookingApparatus != null)
        {
            food.CookingApparatus.IsBusy = true;
            await SleepGenerator.Delay(food.PreparationTime);
            food.CookingApparatus.IsBusy = false;
            cooker.CookingList.Remove(food);
            await ConsoleHelper.Print($"I am {cooker.Name} and cooked {food.Name} from order {order.Id}",
                ConsoleColor.Green);
        }
    }
}