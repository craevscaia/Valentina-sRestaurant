namespace FoodOrderingService.Models;

public class ClientOrderResponse
{
    public int RestaurantId { get; set; }
    public string RestaurantAddress { get; set; }
    public int OrderId { get; set; }
    public int EstimatedWaitingTime { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime RegisteredTime { get; set; }
}