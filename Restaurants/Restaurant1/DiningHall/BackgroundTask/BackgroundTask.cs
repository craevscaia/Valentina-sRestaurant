using DiningHall.DiningHall;

namespace DiningHall.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BackgroundTask(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.Delay(5000, stoppingToken);
            using var scope = _serviceScopeFactory.CreateScope();
            var diningHall = scope.ServiceProvider.GetRequiredService<IDiningHall>();
            await diningHall.InitializeDiningHallParallelAsync();
            await diningHall.MaintainRestaurant(stoppingToken);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}