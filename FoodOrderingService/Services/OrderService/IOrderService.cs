using FoodOrderingService.Models;

namespace FoodOrderingService.Services.OrderService;

public interface IOrderService
{
    Task SeparateOrders(ClientOrder clientOrder);
    Task CheckIfOrderReady(OnlineOrder order);
}