
using Kitchen.Models.Base;

namespace Kitchen.Models.OnlineOrders;

public class ClientOrder : IOrder
{
    public int ClientId { get; set; }
    public OnlineOrder Order { get; set; }
}