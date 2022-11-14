using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.OrderRepository;

public interface IOrderRepository
{
    void InsertOrder(Order order);
    Task<ConcurrentBag<Order>> GetAll();
    Task<Order?> GetById(int id);
    Task<Order?> GetOrderByStatus(OrderStatus status);

    Task<Order?> GetOrderByTableId(int id);
}