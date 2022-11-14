using System.Collections.Concurrent;
using Kitchen.Helpers;
using Kitchen.Mapping;
using Kitchen.Models;
using Kitchen.Models.Online;
using Kitchen.Services.OrderHistoryService;
using Kitchen.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers;

[ApiController]
[Route("/kitchen")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IOrderHistoryService _orderHistoryService;

    public OrderController(IOrderService orderService, IOrderHistoryService orderHistoryService)
    {
        _orderService = orderService;
        _orderHistoryService = orderHistoryService;
    }

    [HttpGet]
    public ConcurrentBag<OrderHistory> GetOrderHistory()
    {
        return _orderHistoryService.GetAll();
    }

    [HttpPost("OnlineOrder")]
    public async Task GetOrderFromKitchen([FromBody] OnlineOrder onlineOrder)
    {
        var order = onlineOrder.Map();
        try
        {
            await ConsoleHelper.Print($"An order with {order.Id} came in the kitchen", ConsoleColor.DarkYellow);
            await _orderService.InsertOrder(order);
        }
        catch (Exception e)
        {
            //ignore
        }
    }
    
    [HttpPost("RestaurantOrder")]
    public async Task GetOrderFromKitchen([FromBody] Order? order)
    {
        if (order == null) return;
        try
        {
            await ConsoleHelper.Print($"An order with {order.Id} came in the kitchen", ConsoleColor.DarkYellow);
            await _orderService.InsertOrder(order);
        }
        catch (Exception e)
        {
            //ignore
        }
    }
}