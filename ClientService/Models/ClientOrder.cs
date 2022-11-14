namespace Client.Models;

public class ClientOrder : IOrder
{
    public int ClientId { get; set; }
    public IList<Order> Orders { get; set; }
}