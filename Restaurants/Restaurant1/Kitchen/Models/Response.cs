using Kitchen.Models.Base;

namespace Kitchen.Models;

public class Response : IOrder
{
    public int OrderId { get; set; }
    public int WaitingTime { get; set; }
}