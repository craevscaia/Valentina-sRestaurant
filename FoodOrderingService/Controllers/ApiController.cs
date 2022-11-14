using System.Collections.Concurrent;
using FoodOrderingService.Helpers;
using FoodOrderingService.Mapping;
using FoodOrderingService.Models;
using FoodOrderingService.Services.ClientOrderService;
using FoodOrderingService.Services.OrderService;
using FoodOrderingService.Services.RestaurantDataService;
using FoodOrderingService.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingService.Controllers;

[ApiController]
[Route("/foodorderingservice")]
public class ApiController : Controller
{
    private readonly IRestaurantDataService _restaurantDataService;
    private readonly IOrderService _orderService;
    private readonly IClientOrderService _clientOrderService;
    private static readonly Semaphore Semaphore = new(1, 1);

    public ApiController(IRestaurantDataService restaurantDataService, IOrderService orderService,
        IClientOrderService clientOrderService)
    {
        _restaurantDataService = restaurantDataService;
        _orderService = orderService;
        _clientOrderService = clientOrderService;
    }

    [HttpGet("/menu")]
    public Task<ConcurrentBag<Restaurant>> GetRestaurants()
    {
        return _restaurantDataService.GetRestaurantData();
    }

    [HttpPost("/register")]
    public Task RegisterRestaurant([FromBody] Restaurant restaurant)
    {
        var restaurantModel = MappingRestaurant.MapRestaurant(restaurant);
        ConsoleHelper.Print($"A new restaurant with id {restaurantModel.RestaurantId} was registered");
        Semaphore.WaitOne();
        _restaurantDataService.Insert(restaurantModel);
        Semaphore.Release();
        return Task.CompletedTask;
    }

    [HttpPost("/order")]
    public Task GetOrderFromClient([FromBody] ClientOrder order)
    {
        Semaphore.WaitOne();
        _clientOrderService.Insert(order);
        _orderService.SeparateOrders(order);
        Semaphore.Release();

        return Task.CompletedTask;
    }

    [HttpPost("/orderready")]
    public async Task GetOrderFromRestaurant([FromBody] OnlineOrder order)
    {
        Semaphore.WaitOne();
        await ConsoleHelper.Print($"I received from the restaurant an order from client {order.ClientId}", ConsoleColor.Cyan);
        Console.WriteLine($"Rating for restaurant {order.RestaurantId} is: {await Rating.ComputeRating(order)}");
        await _orderService.CheckIfOrderReady(order);
        Semaphore.Release();
    }

    [HttpPost("/response")]
    public async Task GetResponseFromRestaurant([FromBody] Response response)
    {
        await ConsoleHelper.Print($"I received from the restaurant an order with id {response.OrderId} and waiting time {response.WaitingTime}",
            ConsoleColor.Cyan);
        Semaphore.WaitOne();
        await ApiHelpers<Response>.SendOrder(response, Setting.ClientUrl);
        Semaphore.Release();
    }
}