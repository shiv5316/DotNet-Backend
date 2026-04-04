using Microsoft.EntityFrameworkCore;
using DeliveryService.API.Data;
using DeliveryService.API.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext



builder.Services.AddDbContext<DeliveryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        }));

var app = builder.Build();


// 🔥 START DELIVERY CONSUMER (IMPORTANT)
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

var consumer = new DeliveryConsumer(scopeFactory);
consumer.Start();


app.Run();