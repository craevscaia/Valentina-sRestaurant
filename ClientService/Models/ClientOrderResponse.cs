namespace Client.Models;

public class ClientOrderResponse
{
    public ClientOrderResponse(int restaurantId, string restaurantAddress, int orderId, int estimatedWaitingTime,
        DateTime createdTime, DateTime registeredTime)
    {
        RestaurantId = restaurantId;
        RestaurantAddress = restaurantAddress;
        OrderId = orderId;
        EstimatedWaitingTime = estimatedWaitingTime;
        CreatedTime = createdTime;
        RegisteredTime = registeredTime;
    }

    private int RestaurantId { get; set; }
    private string RestaurantAddress { get; set; }
    private int OrderId { get; set; }
    private int EstimatedWaitingTime { get; set; }
    private DateTime CreatedTime { get; set; }
    private DateTime RegisteredTime { get; set; }
}