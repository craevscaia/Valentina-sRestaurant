using System.Collections.Concurrent;

namespace DiningHall.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<ConcurrentBag<T>> GetAll();
    Task<T?> GetById(int id);
}