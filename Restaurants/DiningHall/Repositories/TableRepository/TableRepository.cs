using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.GenericRepository;
using DiningHall.SettingsFolder;

namespace DiningHall.Repositories.TableRepository;

public class TableRepository : ITableRepository
{
    private readonly ConcurrentBag<Table> _tables;
    private readonly IGenericRepository<Table> _repository;

    public TableRepository()
    {
        _tables = new ConcurrentBag<Table>();
        _repository = new GenericRepository<Table>(_tables);
    }

    public Task GenerateTables()
    {
        const int maxTables = Settings.NrOfTables;
        for (var id = 1; id < maxTables + 1; id++)
        {
            _tables.Add(
                new Table
                {
                    Id = id,
                    TableStatus = TableStatus.IsAvailable
                });
        }

        return Task.CompletedTask;
    }

    public void InsertTable(Table table)
    {
        _tables.Add(table);
    }

    public Task<ConcurrentBag<Table>> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<Table?> GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Task<Table?> GetTableByStatus(TableStatus status)
    {
        return Task.FromResult(_tables.FirstOrDefault(table => table.TableStatus == status));
    }
}