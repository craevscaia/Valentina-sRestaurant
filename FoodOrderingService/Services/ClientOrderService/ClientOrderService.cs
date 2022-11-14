using FoodOrderingService.Helpers;
using FoodOrderingService.Models;

namespace FoodOrderingService.Services.ClientOrderService;

public class ClientOrderService : IClientOrderService
{
    private readonly List<ClientOrder> _clientOrders;

    public ClientOrderService()
    {
        _clientOrders = new List<ClientOrder>();
    }
    public Task Insert(ClientOrder clientOrder)
    {
        _clientOrders.Add(clientOrder);
        ConsoleHelper.Print($"I added order from client: {clientOrder.ClientId}", ConsoleColor.Cyan);
        return Task.CompletedTask;
    }

    public Task<ClientOrder> GetOrderByClientId(int clientId)
    {
        return Task.FromResult(_clientOrders.First(order => order.ClientId.Equals(clientId)));
    }

    
}