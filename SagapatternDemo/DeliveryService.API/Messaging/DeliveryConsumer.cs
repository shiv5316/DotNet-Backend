namespace DeliveryService.API.Messaging
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using Newtonsoft.Json;
    using System.Text;
    using DeliveryService.API.Data;
    using DeliveryService.API.Entities;
    using Shared.Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public class DeliveryConsumer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DeliveryConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("food-prepared", false, false, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonConvert.DeserializeObject<FoodPreparedEvent>(json);

                var publisher = new RabbitPublisher();

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DeliveryDbContext>();

                    // 🔥 SIMULATE FAILURE (IMPORTANT)
                    if (message.OrderId % 2 == 0)
                    {
                        context.Deliveries.Add(new Delivery
                        {
                            OrderId = message.OrderId,
                            Status = "Failed"
                        });

                        context.SaveChanges();

                        publisher.Publish("delivery-failed", new DeliveryFailedEvent
                        {
                            OrderId = message.OrderId,
                            Reason = "Delivery agent not available",
                            CorrelationId = message.CorrelationId
                        });

                        return;
                    }

                    // ✅ SUCCESS
                    context.Deliveries.Add(new Delivery
                    {
                        OrderId = message.OrderId,
                        Status = "Delivered"
                    });

                    context.SaveChanges();

                    publisher.Publish("food-delivered", new FoodDeliveredEvent
                    {
                        OrderId = message.OrderId,
                        CorrelationId = message.CorrelationId
                    });
                }
            };

            channel.BasicConsume("food-prepared", true, consumer);
        }
    }
}
