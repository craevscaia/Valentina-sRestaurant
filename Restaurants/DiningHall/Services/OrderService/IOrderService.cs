using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    // Task GenerateOrder();
    Task SendOrder(Order order);
    Task<ConcurrentBag<Order>> GetAll();
    Task<Order?> GetOrderByTableId(int id);
}