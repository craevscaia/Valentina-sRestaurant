using DiningHall.DiningHall;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.GenericRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services.FoodService;
using DiningHall.Services.OrderService;
using DiningHall.Services.RegisterRestaurantService;
using DiningHall.Services.TableRepository;
using DiningHall.Services.WaiterService;

namespace DiningHall;

public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddLogging(config => config.ClearProviders());

        
        services.AddSingleton<IWaiterRepository, WaiterRepository>();
        services.AddSingleton<ITableRepository, TableRepository>();
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton<IFoodRepository, FoodRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddSingleton<IWaiterService, WaiterService>();
        services.AddSingleton<ITableService, TableService>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IFoodService, FoodService>();
        
        services.AddSingleton<IRegisterRestaurantService, RegisterRestaurantService>();
        
        services.AddSingleton<IDiningHall, DiningHall.DiningHall>();
        services.AddHostedService<BackgroundTask.BackgroundTask>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}