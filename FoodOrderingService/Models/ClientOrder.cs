namespace FoodOrderingService.Models;

public class ClientOrder : IOrder
{
    public int ClientId { get; set; }
    public List<Order> Orders{ get; set; }
}