namespace Client.Helpers;

public static class IdGenerator
{
    private static readonly Semaphore ClientSemaphore = new(1, 1);
    private static readonly Semaphore OrderSemaphore = new(1, 1);
    private static int ClientId { get; set; }
    private static int OrderId { get; set; }

    public static Task<int> GenerateClientId()
    {
        ClientSemaphore.WaitOne();
        ClientId += 1;
        ClientSemaphore.Release();
        return Task.FromResult(ClientId);
    }
    public static Task<int> GenerateOrderId()
    {
        OrderSemaphore.WaitOne();
        OrderId += 1;
        OrderSemaphore.Release();
        return Task.FromResult(OrderId);
    }
}