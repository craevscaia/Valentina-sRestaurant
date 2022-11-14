namespace DiningHall.DiningHall;

public interface IDiningHall
{
    Task InitializeDiningHallParallelAsync();
    Task MaintainRestaurant(CancellationToken stoppingToken);
}