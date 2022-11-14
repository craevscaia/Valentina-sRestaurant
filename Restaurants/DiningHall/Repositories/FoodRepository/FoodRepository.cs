using DiningHall.Models;
using DiningHall.SettingsFolder;
using Newtonsoft.Json;

namespace DiningHall.Repositories.FoodRepository;

public class FoodRepository : IFoodRepository
{
    private IList<Food> _foods;

    public FoodRepository()
    {
        _foods = new List<Food>();
    }

    public async Task GenerateMenu()
    {
        using var streamReader = new StreamReader(Settings.Menu);
        var json = await streamReader.ReadToEndAsync();
        _foods = JsonConvert.DeserializeObject<List<Food>>(json)!;
    }
    
    public Task<IList<Food>> GetAll()
    {
        return Task.FromResult(_foods);
    }

    public Task<Food?> GetById(int id)
    {
        return Task.FromResult(_foods.FirstOrDefault(t => t.Id.Equals(id)));
    }
}