namespace FoodOrderingService.Models;

public class OnlineOrder : IOrder
{
    public int OrderId { get; set; }
    public int RestaurantId { get; set; }
    public int ClientId { get; set; }
    public IList<int> Foods { get; set; }
    public int Priority { get; set; }
    public int MaxWait { get; set; }
    public DateTime CreateOnTime { get; set; }
}