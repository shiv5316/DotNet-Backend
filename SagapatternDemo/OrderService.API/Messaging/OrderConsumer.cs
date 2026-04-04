
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using Newtonsoft.Json;
    using System.Text;
    using OrderService.API.Data;
    using Shared.Contracts;

namespace OrderService.API.Messaging
{
    public class OrderConsumer
    {
        private readonly AppDbContext _context;

        public OrderConsumer(AppDbContext context)
        {
            _context = context;
        }

        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("food-delivered", false, false, false);
            channel.QueueDeclare("food-failed", false, false, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                if (ea.RoutingKey == "food-delivered")
                {
                    var msg = JsonConvert.DeserializeObject<FoodDeliveredEvent>(json);
                    var order = _context.Orders.Find(msg.OrderId);
                    order.Status = "Completed";
                }
                else
                {
                    var msg = JsonConvert.DeserializeObject<FoodPreparationFailedEvent>(json);
                    var order = _context.Orders.Find(msg.OrderId);
                    order.Status = "Cancelled";
                }

                await _context.SaveChangesAsync();
            };

            channel.BasicConsume("food-delivered", true, consumer);
            channel.BasicConsume("food-failed", true, consumer);
        }
    }
}
