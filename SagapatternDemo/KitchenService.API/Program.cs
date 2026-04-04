using Microsoft.EntityFrameworkCore;
using KitchenService.API.Data;
using KitchenService.API.Messaging;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<KitchenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Start consumer
using (var scope = app.Services.CreateScope())
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    var consumer = new KitchenConsumer(scopeFactory);
    consumer.Start();
}

app.Run();