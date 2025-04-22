using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadawca
{
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

                for (int i = 0; i < 10; i++)
                {

                    string text = $"Wiadomość {i} od Nadawcy";
                    var body = Encoding.UTF8.GetBytes(text);

                    channel.BasicPublish("", "message_queue", null, body);
                }
                Console.WriteLine("Wiadomości wysłane");
                Console.ReadKey();
            }
        }
    }
}
