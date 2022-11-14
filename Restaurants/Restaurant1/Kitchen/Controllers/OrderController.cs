using Kitchen.Helpers;
using Kitchen.Models;
using Kitchen.Models.OnlineOrders;
using Kitchen.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers;

[ApiController]
[Route("/kitchen")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private static readonly Semaphore Semaphore = new(1, 1);

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost]
    public async Task GetOrderFromRestaurant([FromBody] Order? order)
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

    [HttpPost("/response")]
    public async Task GetResponseFromKitchen([FromBody] OnlineOrder order)
    {
        try
        {
            await ConsoleHelper.Print($"Preparing waiting time for order {order.OrderId} from client {order.ClientId}",
                ConsoleColor.DarkYellow);
            await _orderService.PrepareOrderResponse(order);
        }
        catch (Exception e)
        {
            //ignore
        }
    }
}