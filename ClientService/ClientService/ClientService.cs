using Client.Helpers;
using Client.Models;
using Client.Service.OrderService;

namespace Client.ClientService;

public class ClientService : IClientService
{
    public int ActiveClients = 0;
    private readonly IOrderService _orderService;
    private static readonly Semaphore Semaphore = new(1, 1);


    public ClientService(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task ExecuteCode(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var taskList = new List<Task>
            {
                Task.Run(GenerateOrder, cancellationToken),
                // Task.Run(GenerateOrder, cancellationToken),
                // Task.Run(GenerateOrder, cancellationToken),
            };
            await Task.WhenAll(taskList);
        }
    }

    private async Task GenerateOrder()
    {
        Semaphore.WaitOne();
        var clientId = await IdGenerator.GenerateClientId();
        var clientOrder = await _orderService.CreateOrder(clientId);
        await ApiHelpers<ClientOrder>.SendOrder(clientOrder, Setting.FosPlaceOrderUrl);
        Semaphore.Release();
        await ConsoleHelper.Print($"I just have sent order from client {clientOrder.ClientId}...");
        await SleepGenerator.Delay(60);

    }
}