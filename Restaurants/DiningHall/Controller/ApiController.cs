using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.TableRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controller;

[ApiController]
[Route("/dininghall")]
public class ApiController : ControllerBase
{
    private readonly ITableRepository _tableRepository;
    private readonly Semaphore _semaphore;
    
    public ApiController(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
        _semaphore = new Semaphore(1, 1);
    }

    [HttpPost("/order")]
    public async Task GetOrderFromFoodOrderingService([FromBody] ClientOrder order)
    {
        await ConsoleHelper.Print(
            $"I received from the foodOrderingService an order",
            ConsoleColor.Cyan);
    }

    [HttpPost]
    public async Task GetOrderFromKitchen([FromBody] Order order)
    {
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
}