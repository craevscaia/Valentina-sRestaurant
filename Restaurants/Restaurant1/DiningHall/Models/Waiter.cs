using System.Collections.Concurrent;
using DiningHall.Models.Base;

namespace DiningHall.Models;

public class Waiter : BaseEntity
{
    public Waiter()
    {
        Name = "";
        Order = new Order();
        ActiveOrders = new ConcurrentBag<Order>();
        NrOfOrdersCompleted = new ConcurrentBag<int>();
    }

    public string Name { get; set; }
    public bool IsFree { get; set; }
    public Order Order { get; set; }    
    public ConcurrentBag<Order> ActiveOrders { get; set; }
    public ConcurrentBag<int> NrOfOrdersCompleted { get; set; }
}