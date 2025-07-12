using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Rinha2025.Backend.Settings;
using System.Text;

namespace Rinha2025.Backend.MessageBus
{
    public class RabbitMqPublisher : IMessageBus
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqSettings _rabbitMqOptions;

        public RabbitMqPublisher(IOptions<RabbitMqSettings> opts)
        {
            _rabbitMqOptions = opts.Value;
            _factory = new ConnectionFactory() { HostName = _rabbitMqOptions.HostName };
        }

        public async Task Publish(string message)
        {
            using (var connection = await _factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: _rabbitMqOptions.Queue, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _rabbitMqOptions.Queue, body: body, mandatory: true);
            }
        }
    }
}
