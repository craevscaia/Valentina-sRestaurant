using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.TableRepository;

public interface ITableRepository
{
    void InsertTable(Table table);
    Task GenerateTables();
    Task<ConcurrentBag<Table>> GetAll();
    Task<Table?> GetById(int id);
    Task<Table?> GetTableByStatus(TableStatus status);
}