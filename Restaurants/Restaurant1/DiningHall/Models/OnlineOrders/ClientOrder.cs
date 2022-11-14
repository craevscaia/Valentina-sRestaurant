using DiningHall.Models.Base;

namespace DiningHall.Models.OnlineOrders;

public class ClientOrder : IOrder
{
    public int ClientId { get; set; }
    public OnlineOrder Order { get; set; }
}