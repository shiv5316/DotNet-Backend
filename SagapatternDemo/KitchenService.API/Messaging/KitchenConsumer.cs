namespace KitchenService.API.Messaging
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using Newtonsoft.Json;
    using System.Text;
    using KitchenService.API.Data;
    using KitchenService.API.Entities;
    using Shared.Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public class KitchenConsumer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public KitchenConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // Declare queues
            channel.QueueDeclare("order-created", false, false, false);
            channel.QueueDeclare("delivery-failed", false, false, false);

            var publisher = new RabbitPublisher();

            // ===============================
            // ✅ CONSUMER 1 — order-created
            // ===============================
            var orderConsumer = new EventingBasicConsumer(channel);

            orderConsumer.Received += (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonConvert.DeserializeObject<OrderCreatedEvent>(json);

                // ❌ FAILURE CASE (Paneer)
                if (message.Items.Contains("paneer"))
                {
                    publisher.Publish("food-failed", new FoodPreparationFailedEvent
                    {
                        OrderId = message.OrderId,
                        Reason = "Paneer not available",
                        CorrelationId = message.CorrelationId
                    });

                    return;
                }

                // ✅ SUCCESS CASE
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<KitchenDbContext>();

                    var kitchenOrder = new KitchenOrder
                    {
                        OrderId = message.OrderId,
                        Items = message.Items,
                        Status = "Prepared"
                    };

                    context.KitchenOrders.Add(kitchenOrder);
                    context.SaveChanges();
                }

                publisher.Publish("food-prepared", new FoodPreparedEvent
                {
                    OrderId = message.OrderId,
                    CorrelationId = message.CorrelationId
                });
            };

            channel.BasicConsume("order-created", true, orderConsumer);


            // =====================================
            // 🔥 CONSUMER 2 — delivery-failed (NEW)
            // =====================================
            var deliveryFailedConsumer = new EventingBasicConsumer(channel);

            deliveryFailedConsumer.Received += (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonConvert.DeserializeObject<DeliveryFailedEvent>(json);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<KitchenDbContext>();

                    var order = context.KitchenOrders
                        .FirstOrDefault(x => x.OrderId == message.OrderId);

                    if (order != null)
                    {
                        order.Status = "Cancelled";
                        context.SaveChanges();
                    }
                }

                // Notify OrderService
                publisher.Publish("food-failed", new FoodPreparationFailedEvent
                {
                    OrderId = message.OrderId,
                    Reason = "Cancelled due to delivery failure",
                    CorrelationId = message.CorrelationId
                });
            };

            channel.BasicConsume("delivery-failed", true, deliveryFailedConsumer);
        }
    }
}
