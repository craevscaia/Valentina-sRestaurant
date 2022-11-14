using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Repositories.GenericRepository;
using DiningHall.SettingsFolder;

namespace DiningHall.Repositories.WaiterRepository;

public class WaiterRepository : IWaiterRepository
{
    private readonly ConcurrentBag<Waiter> _waiters;
    private readonly IGenericRepository<Waiter> _repository;

    public WaiterRepository()
    {
        _waiters = new ConcurrentBag<Waiter>();
        _repository = new GenericRepository<Waiter>(_waiters);
    }

    public Task GenerateWaiters()
    {
        const int nrOfWaiters = Settings.NrOfWaiters;
        for (var id = 1; id < nrOfWaiters + 1; id++)
        {
            _waiters.Add(new Waiter
            {
                Id = id,
                Name = $"Waiter {id}",
                IsFree = true,
            });
        }

        return Task.CompletedTask;
    }

    public void InsertWaiter(Waiter waiter)
    {
        _waiters.Add(waiter);
    }

    public Task<ConcurrentBag<Waiter>> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<Waiter?> GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Task<Waiter?> GetFreeWaiter()
    {
        return Task.FromResult(_waiters.FirstOrDefault(waiter => waiter.IsFree));
    }
}