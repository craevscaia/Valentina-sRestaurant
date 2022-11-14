using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    Task<ConcurrentBag<Waiter>> GetAll();
    Task<Waiter?> GetById(int id);
    Task ServeTable();
    Task GenerateWaiters();
}