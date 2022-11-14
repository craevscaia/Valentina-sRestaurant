using Client.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Client.Repositories.RestaurantDataRepository;

public class RestaurantDataRepository : IRestaurantDataRepository
{
    private IList<RestaurantData> RestaurantData { get; set; }

    public RestaurantDataRepository()
    {
        RestaurantData = new List<RestaurantData>();
    }

    public void SetRestaurantData(IList<RestaurantData> restaurantData)
    {
        RestaurantData = restaurantData;
    }

    [HttpGet]
    public Task<IList<RestaurantData>?> GetRestaurantData()
    {
        var client = new RestClient(Setting.GetMenuUrl);
        var response = client.Execute<IList<RestaurantData>>(new RestRequest());
        if (response.Data != null)
        {
            RestaurantData = response.Data;
        }
        return Task.FromResult(response.Data);
    }

    public Task Insert(RestaurantData restaurantData)
    {
        RestaurantData.Add(restaurantData);
        return Task.CompletedTask;
    }

    public Task<RestaurantData> GetRestaurantDataById(int randomRestaurant)
    {
        return Task.FromResult(RestaurantData.FirstOrDefault(data => data.RestaurantId.Equals(randomRestaurant)))!;
    }
}