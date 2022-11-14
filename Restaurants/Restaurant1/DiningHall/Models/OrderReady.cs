namespace DiningHall.Models;

public class OrderReady
{
    public int OrderId { get; set; }
    public int ClientId { get; set; }
    public IEnumerable<Order> Orders{ get; set; }
}