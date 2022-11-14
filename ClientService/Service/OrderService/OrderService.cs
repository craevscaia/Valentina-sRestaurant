using Client.Helpers;
using Client.Models;
using Client.Service.RestaurantDataService;

namespace Client.Service.OrderService;

public class OrderService : IOrderService
{
    private readonly IRestaurantDataService _restaurantDataService;

    public OrderService(IRestaurantDataService restaurantDataService)
    {
        _restaurantDataService = restaurantDataService;
    }

    public async Task<ClientOrder> CreateOrder(int clientId)
    {
        var orders = new List<Order>();
        var restaurantDataList = await _restaurantDataService.GetRestaurantData();

        var chooseFoodFromNRestaurants =
            await RandomGenerator.ListNumberGenerator(restaurantDataList.Count); //order for max 3 restaurants at a time
        foreach (var restaurantId in chooseFoodFromNRestaurants)
        {
            var orderId = await IdGenerator.GenerateOrderId();
            var restaurant = await _restaurantDataService.GetRestaurantDataById(restaurantId);
            var menu = restaurant.Menu.ToList();
            var randomIdsFoodList = await RandomGenerator.ListNumberGenerator(menu.Count);
            var randomFoodList = await GetFoodFromRandomIdsList(menu, randomIdsFoodList);
            orders.Add(new Order
            {
                OrderId = orderId,
                RestaurantId = restaurant.RestaurantId,
                ClientId = clientId,
                Foods = randomIdsFoodList.ToList(),
                Priority = await RandomGenerator.NumberGenerator(3),
                MaxWait = await MaxWaitingTime.CalculateMaxWaitingTine(randomFoodList),
                CreateOnTime = DateTime.Now
            });
        }

        return new ClientOrder
        {
            ClientId = clientId,
            Orders = orders,
        };
    }

    private static Task<IList<Food>> GetFoodFromRandomIdsList(IReadOnlyList<Food> foods, IEnumerable<int> randomFoodList)
    {
        return Task.FromResult<IList<Food>>(randomFoodList.Select(id => foods[id]).ToList());
    }
}