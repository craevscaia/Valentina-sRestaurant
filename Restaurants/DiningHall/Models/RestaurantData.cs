namespace DiningHall.Models;

public class RestaurantData
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int MenuItems { get; set; }
    public IEnumerable<Food> Menu { get; set; }
    public int Raiting { get; set; }
}