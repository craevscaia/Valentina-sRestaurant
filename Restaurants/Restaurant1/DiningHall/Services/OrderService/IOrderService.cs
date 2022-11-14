using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.OnlineOrders;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    Task GenerateOrder();
    Task<ConcurrentBag<Order>> GetAll();
    Task<Order?> GetOrderByTableId(int id);
}