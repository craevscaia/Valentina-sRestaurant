using Client.Models;

namespace Client.Service.OrderService;

public interface IOrderService
{
    Task<ClientOrder> CreateOrder(int clientId);
}