using System.Collections.Concurrent;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services.OrderService;
using DiningHall.SettingsFolder;

namespace DiningHall.Services.WaiterService;

public class WaiterService : IWaiterService
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IOrderService _orderService;
    private readonly Semaphore _semaphore;

    public WaiterService(IOrderService orderService, IWaiterRepository waiterRepository,
        ITableRepository tableRepository)
    {
        _orderService = orderService;
        _waiterRepository = waiterRepository;
        _tableRepository = tableRepository;
        _semaphore = new Semaphore(1, 1);
    }

    public Task GenerateWaiters()
    {
        return _waiterRepository.GenerateWaiters();
    }

    public Task<ConcurrentBag<Waiter>> GetAll()
    {
        return _waiterRepository.GetAll();
    }

    public Task<Waiter?> GetById(int id)
    {
        return _waiterRepository.GetById(id);
    }

    private Task<Waiter?> GetFreeWaiter()
    {
        return _waiterRepository.GetFreeWaiter();
    }

    public async Task ServeTable()
    {
        _semaphore.WaitOne();
        var waiter = await GetFreeWaiter();
        if (waiter != null)
        {
            var table = await _tableRepository.GetTableByStatus(TableStatus.WaitingForWaiter);
            _semaphore.Release();
            if (table != null)
            {
                var order = await _orderService.GetOrderByTableId(table.Id);

                if (order != null)
                {
                    order.WaiterId = waiter.Id;

                    waiter.Order = order;
                    waiter.IsFree = false;
                    waiter.ActiveOrders.Add(order);

                    await ConsoleHelper.Print(
                        $"I am {waiter.Name} and I drive order {order.Id} in the kitchen from table {table.Id}", ConsoleColor.Blue);
                    await ApiHelpers<Order>.SendOrder(order, Settings.KitchenUrl);

                    table.TableStatus = TableStatus.WaitingForOrderToBeServed;
                    await SleepWaiter(waiter);
                }
            }
            else
            {
                var sleepTime = RandomGenerator.NumberGenerator(10);
                await SleepGenerator.Delay(sleepTime);
            }
        }
        else
        {
            var sleepTime = RandomGenerator.NumberGenerator(SleepingSetting.NoFreeWaitersMin, SleepingSetting.NoFreeWaitersMax);
            await ConsoleHelper.Print("There are no free waiters now", ConsoleColor.Red);
            await SleepGenerator.Delay(sleepTime);
        }
    }

    private static async Task SleepWaiter(Waiter waiter)
    {
        var sleepTime = RandomGenerator.NumberGenerator(SleepingSetting.RestWaiterForMin, SleepingSetting.RestWaiterForMax);
        await ConsoleHelper.Print($"I am waiter {waiter.Name}. I will rest for {sleepTime} seconds", ConsoleColor.Yellow);
        await SleepGenerator.Delay(sleepTime);
        await ConsoleHelper.Print($"{waiter.Name} is ready for a new order", ConsoleColor.Green);
        waiter.IsFree = true;
    }
}