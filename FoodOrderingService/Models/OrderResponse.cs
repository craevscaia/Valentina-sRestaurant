namespace FoodOrderingService.Models;

public class OrderResponse : IOrder
{
    public int OrderId { get; set; }
    public IEnumerable<ClientOrderResponse> Orders { get; set; }
}