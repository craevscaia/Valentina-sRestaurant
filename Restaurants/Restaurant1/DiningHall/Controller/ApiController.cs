using DiningHall.Helpers;
using DiningHall.Mapping;
using DiningHall.Models;
using DiningHall.Models.OnlineOrders;
using DiningHall.Models.Status;
using DiningHall.Repositories.TableRepository;
using DiningHall.Services.OrderService;
using DiningHall.SettingsFolder;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controller;

[ApiController]
[Route("/dininghall")]
public class ApiController : ControllerBase
{
    private readonly ITableRepository _tableRepository;
    private readonly IOrderService _orderService;
    private readonly Semaphore _semaphore;

    public ApiController(ITableRepository tableRepository, IOrderService orderService)
    {
        _tableRepository = tableRepository;
        _orderService = orderService;
        _semaphore = new Semaphore(1, 1);
    }

    [HttpPost]
    public async Task GetOrderFromKitchen([FromBody] Order order)
    {
        if (order.TableId == null || order.WaiterId == null)
        {
            var onlineOrder = order.MapOnlineOrder();
            await ApiHelpers<OnlineOrder>.SendOrder(onlineOrder, Settings.FoodOrderingServiceReceiveOrderUrl);

        }
        
        order.OrderStatus = OrderStatus.OrderCooked;
        var table = await _tableRepository.GetById(order.TableId);
        if (table != null)
        {
            table.TableStatus = TableStatus.IsAvailable;
            await ConsoleHelper.Print(
                $"I received from the kitchen an order with id {order.Id} for table {order.TableId}",
                ConsoleColor.Cyan);
            _semaphore.WaitOne();
            await RatingHelper.GetRating(order);
            _semaphore.Release();
        }
    }

    [HttpPost("/order")]
    public async Task GetOrderFromFoodOrderingService([FromBody] OnlineOrder onlineOrder)
    {
        await ConsoleHelper.Print($"I received from the foodOrderingService an order", ConsoleColor.Cyan);
        var order = onlineOrder.MapOrder();
        await ApiHelpers<Order>.SendOrder(order, Settings.KitchenUrl);
        await ApiHelpers<OnlineOrder>.SendOrder(onlineOrder, Settings.KitchenUrlResponse);
    }

    [HttpPost("/response")]
    public async Task GetResponseFromKitchen([FromBody] Response response)
    {
        await ConsoleHelper.Print(
            $"I received from the kitchen waiting time {response.WaitingTime} for orderWith id {response.OrderId}",
            ConsoleColor.Cyan);
        await ApiHelpers<Response>.SendOrder(response, Settings.FoodOrderingServiceResponseUrl);
    }
}