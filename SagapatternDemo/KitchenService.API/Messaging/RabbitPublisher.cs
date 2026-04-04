namespace KitchenService.API.Messaging
{
    using RabbitMQ.Client;
    using Newtonsoft.Json;
    using System.Text;

    public class RabbitPublisher
    {
        public void Publish(string queue, object message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue, false, false, false);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", queue, null, body);
        }
    }
}
