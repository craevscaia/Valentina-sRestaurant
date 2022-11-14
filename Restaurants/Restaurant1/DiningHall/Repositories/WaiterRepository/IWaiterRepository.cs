using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public interface IWaiterRepository
{
    Task GenerateWaiters();
    void InsertWaiter(Waiter waiter);
    Task<ConcurrentBag<Waiter>> GetAll();
    Task<Waiter?> GetById(int id);
    Task<Waiter?> GetFreeWaiter();
}