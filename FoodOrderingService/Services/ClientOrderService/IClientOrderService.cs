using FoodOrderingService.Models;

namespace FoodOrderingService.Services.ClientOrderService;

public interface IClientOrderService
{
    Task Insert(ClientOrder clientOrder);
    Task<ClientOrder> GetOrderByClientId(int clientId);
}