using FoodOrderingService.Helpers;
using FoodOrderingService.Models;
using FoodOrderingService.Services.ClientOrderService;
using FoodOrderingService.Services.RestaurantDataService;
using FoodOrderingService.Settings;

namespace FoodOrderingService.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IRestaurantDataService _restaurantDataService;
    private readonly IClientOrderService _clientOrderService;

    public OrderService(IRestaurantDataService restaurantDataService, IClientOrderService clientOrderService)
    {
        _restaurantDataService = restaurantDataService;
        _clientOrderService = clientOrderService;
    }

    public async Task SeparateOrders(ClientOrder clientOrder)
    {
        foreach (var order in clientOrder.Orders)
        {
            var url = _restaurantDataService.GetRestaurantAddressById(order.RestaurantId);
            await ApiHelpers<Order>.SendOrder(order, url);
        }
    }
    
    public async Task CheckIfOrderReady(OnlineOrder onlineOrder)
    {
        var order = await _clientOrderService.GetOrderByClientId(onlineOrder.ClientId);
        var readyOrder = order.Orders.First(order1 => order1.OrderId.Equals(onlineOrder.OrderId));
        order.Orders.Remove(readyOrder);

        if (order.Orders.Count == 0)
        {
            await ApiHelpers<OnlineOrder>.SendOrder(onlineOrder, Setting.ClientOrderReady);
        }
    }
}