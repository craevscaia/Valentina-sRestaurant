using DiningHall.Models.Base;

namespace DiningHall.Models;

public class Response : IOrder
{
    public int OrderId { get; set; }
    public int WaitingTime { get; set; }
}