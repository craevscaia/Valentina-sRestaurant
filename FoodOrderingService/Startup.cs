using FoodOrderingService.Repositories.RestaurantDataRepository;
using FoodOrderingService.Services.ClientOrderService;
using FoodOrderingService.Services.OrderService;
using FoodOrderingService.Services.RestaurantDataService;

namespace FoodOrderingService;

public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRazorPages();
        services.AddLogging(config => config.ClearProviders());
        
        services.AddSingleton<IRestaurantDataRepository, RestaurantDataRepository>();
        services.AddSingleton<IRestaurantDataRepository, RestaurantDataRepository>();
        services.AddSingleton<IRestaurantDataService, RestaurantDataService>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IClientOrderService, ClientOrderService>();
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}