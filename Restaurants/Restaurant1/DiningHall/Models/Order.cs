using DiningHall.Models.Base;
using DiningHall.Models.Status;
using Newtonsoft.Json;

namespace DiningHall.Models;

public class Order : Entity, IOrder
{
    public int ClientId { get; set; }
    public int? TableId { get; set; }
    public int? WaiterId { get; set; }
    public int Priority { get; set; }
    public int MaxWait { get; set; }
    public bool OrderIsComplete { get; set; }
    public IList<int> FoodList { get; set; }
    [JsonIgnore] public OrderStatus OrderStatus { get; set; }
}