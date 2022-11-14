using System.Collections.Concurrent;
using System.Net;
using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.OnlineOrders;
using DiningHall.Models.Status;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Services.FoodService;
using DiningHall.Services.TableRepository;
using DiningHall.SettingsFolder;
using Newtonsoft.Json;

namespace DiningHall.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ITableService _tableService;
    private readonly IFoodService _foodService;

    public OrderService(IOrderRepository orderRepository, IFoodService foodService, ITableService tableService)
    {
        _orderRepository = orderRepository;
        _foodService = foodService;
        _tableService = tableService;
    }

    public async Task GenerateOrder()
    {
        var table = await _tableService.GetTableByStatus(TableStatus.IsAvailable);
    
        if (table != null)
        {
            var foodList = await _foodService.GenerateOrderFood();
            var order = new Order
            {
                Id = await IdGenerator.GenerateId(),
                TableId = table.Id,
                Priority = RandomGenerator.NumberGenerator(5),
                CreatedOnUtc = DateTime.Now,
                OrderIsComplete = false,
                FoodList = foodList,
                MaxWait = foodList.CalculateMaximWaitingTime(_foodService),
            };
    
            table.OrderId = order.Id;
            table.TableStatus = TableStatus.WaitingForWaiter;
    
            _orderRepository.InsertOrder(order);
            var sleepingTime = RandomGenerator.NumberGenerator(SleepingSetting.GenerateOrderOnceInMin,
                SleepingSetting.GenerateOrderOnceInMax);
            await ConsoleHelper.Print($"A order with id {order.Id} was generated," +
                                      $"the next order in: {sleepingTime} seconds", ConsoleColor.Yellow);
            await SleepGenerator.Delay(sleepingTime);
        }
        else
        {
            var tableWithSmallestWaitingTime = await _tableService.GetTableWithSmallestWaitingTime();
            if (tableWithSmallestWaitingTime != null)
            {
                var order = await GetById(tableWithSmallestWaitingTime.OrderId);
                if (order != null)
                {
                    await SleepGenerator.Delay(order.MaxWait);
                }
            }
        }
    }

    public Task<ConcurrentBag<Order>> GetAll()
    {
        return _orderRepository.GetAll();
    }

    private Task<Order?> GetById(int id)
    {
        return _orderRepository.GetById(id);
    }
    
    public Task<Order?> GetOrderByTableId(int id)
    {
        return _orderRepository.GetOrderByTableId(id);
    }
}