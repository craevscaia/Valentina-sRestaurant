using DiningHall.Models;

namespace DiningHall.Services.FoodService;

public interface IFoodService
{
     Task<List<int>> GenerateOrderFood();
     Task<IList<Food>> GetAll();
     Task<Food?> GetById(int id);

     Task GenerateMenu();
}