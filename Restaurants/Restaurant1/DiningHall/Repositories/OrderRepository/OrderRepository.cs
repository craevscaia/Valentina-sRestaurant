using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.GenericRepository;

namespace DiningHall.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly ConcurrentBag<Order> _orders;
    
    private readonly IGenericRepository<Order> _repository;

    public OrderRepository()
    {
        _orders = new ConcurrentBag<Order>();
        _repository = new GenericRepository<Order>(_orders);
    }

    public void InsertOrder(Order order)
    {
        _orders.Add(order);
    }

    public Task<ConcurrentBag<Order>> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<Order?> GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Task<Order?> GetOrderByStatus(OrderStatus status)
    {
        return Task.FromResult(_orders.FirstOrDefault(order => order.OrderStatus == status));
    }

    public Task<Order?> GetOrderByTableId(int id)
    {
        return Task.FromResult(_orders.FirstOrDefault(order => order.TableId.Equals(id)));
    }
}