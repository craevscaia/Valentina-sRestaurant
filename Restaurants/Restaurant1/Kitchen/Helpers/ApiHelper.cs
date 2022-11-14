using System.Net;
using System.Text;
using Kitchen.Models.Base;
using Newtonsoft.Json;

namespace Kitchen.Helpers;

public abstract class ApiHelper<T> where T : IOrder
{
    public static async Task SendOrder(T order, string url)
    {
        try
        {
            var serializeObject = JsonConvert.SerializeObject(order);
            var data = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                await ConsoleHelper.Print($"The order was driven to kitchen");
            }
        }
        catch (Exception e)
        {
            await ConsoleHelper.Print($"Failed to send order", ConsoleColor.Red);
        }
    }
}