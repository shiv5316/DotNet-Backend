using Microsoft.EntityFrameworkCore;
using OrderService.API.Data;
using OrderService.API.Middleware;
using OrderService.API.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// ✅ Use your appsettings.json connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// Middleware (only here)
app.UseMiddleware<CorrelationMiddleware>();

// Map controllers
app.MapControllers();

// Start RabbitMQ consumer
var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

var consumer = new OrderConsumer(dbContext);
consumer.Start();

app.Run();