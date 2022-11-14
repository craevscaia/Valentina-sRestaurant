using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Services.TableRepository;

public interface ITableService
{
    Task<ConcurrentBag<Table>> GetAll();
    Task<Table?> GetById(int id);
    Task<Table?> GetTableByStatus(TableStatus status);
    Task<Table?> GetTableWithSmallestWaitingTime();
    Task GenerateTables();
}