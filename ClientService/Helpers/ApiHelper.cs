using System.Net;
using System.Text;
using Client.Models;
using Newtonsoft.Json;

namespace Client.Helpers;

public abstract class ApiHelpers<T> where T : IOrder
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
                await ConsoleHelper.Print($"The order was sent successfully");
            }
        }
        catch (Exception e)
        {
            await ConsoleHelper.Print($"Failed to send order", ConsoleColor.Red);
        }
    }
}