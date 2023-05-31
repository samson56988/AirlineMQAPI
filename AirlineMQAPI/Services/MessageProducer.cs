using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AirlineMQAPI.Services
{
    public class MessageProducer : IMessageProducer  
    {
        public void SendingMessage<T>(T message)
        {
            ConnectionFactory factory= new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

            var conn =  factory.CreateConnection();
            using var channel = conn.CreateModel();
            channel.QueueDeclare("bookings", false, false, false, null);  

            var jsonString = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "bookings", null,body);

        }
    }
}
