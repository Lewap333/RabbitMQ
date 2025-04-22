using RabbitMQ.Client;
using System;
using System.Text;
using System.Xml.Linq;


namespace Odbiorca
{
    class MyConsumer : DefaultBasicConsumer
    {
        public MyConsumer(IModel model) : base(model)
        {
        }

        public override void HandleBasicDeliver(
        string consumerTag,
        ulong deliveryTag,
        bool redelivered,
        string exchange,
        string routingKey,
        IBasicProperties properties,
        byte[] body)
        {
          
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Odebrana wiadomość: {message}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "kebnekaise.lmq.cloudamqp.com",
                VirtualHost = "derzxpib",
                UserName = "derzxpib",
                Password = "41TnllDwLvifJRxipPALriNKQfTGq7Hx",
                Port = 5672,
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("message_queue", false, false, false, null);

                var odbiorca = new MyConsumer(channel);

                channel.BasicConsume("message_queue", true, odbiorca);

                Console.WriteLine("Odbiorca nasłuchuje na wiadomości");
                Console.ReadKey();
            }
        }
    }
}
