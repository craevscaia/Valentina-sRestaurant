using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public interface IFoodRepository
{
    Task GenerateMenu();
    public Task<IList<Food>> GetAll();
    public Task<Food?> GetById(int id);
}