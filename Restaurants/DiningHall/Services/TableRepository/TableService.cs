using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;

namespace DiningHall.Services.TableRepository;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IOrderRepository _orderRepository;

    public TableService(ITableRepository tableRepository, IOrderRepository orderRepository)
    {
        _tableRepository = tableRepository;
        _orderRepository = orderRepository;
    }

    public Task<ConcurrentBag<Table>> GetAll()
    {
        return _tableRepository.GetAll();
    }
    
    public Task<Table?> GetById(int id)
    {
        return _tableRepository.GetById(id);
    }

    public Task<Table?> GetTableByStatus(TableStatus status)
    {
        return _tableRepository.GetTableByStatus(status);
    }

    public async Task<Table?> GetTableWithSmallestWaitingTime()
    {
        var orders = await _orderRepository.GetAll();
        var orderWithMinWaitingTime = orders.MinBy(order => order.MaxWait);
        return await GetById(orderWithMinWaitingTime!.TableId);
    }

    public Task GenerateTables()
    {
        return _tableRepository.GenerateTables();
    }

    public Task ChangeTableStatus(Table table, TableStatus status)
    {
        table.TableStatus = status;
        return Task.CompletedTask;
    }
}