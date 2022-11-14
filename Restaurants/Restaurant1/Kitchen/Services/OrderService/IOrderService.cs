using System.Collections.ObjectModel;
using Kitchen.Models;
using Kitchen.Models.OnlineOrders;

namespace Kitchen.Services.OrderService;

public interface IOrderService
{
    Task InsertOrder(Order order);
    Task<ObservableCollection<Order>> GetAll();
    Task PrepareOrder();
    Task PrepareOrderResponse(OnlineOrder order);
}