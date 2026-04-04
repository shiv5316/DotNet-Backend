using Microsoft.AspNetCore.Mvc;
using OrderService.API.Data;
using OrderService.API.DTOs;
using OrderService.API.Entities;
using OrderService.API.Messaging;
using Shared.Contracts;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        var order = new Order
        {
            Items = dto.Items,
            Status = "Created"
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var publisher = new RabbitPublisher();

        publisher.Publish("order-created", new OrderCreatedEvent
        {
            OrderId = order.Id,
            Items = order.Items,
            CorrelationId = correlationId
        });

        return Ok(order);
    }
}