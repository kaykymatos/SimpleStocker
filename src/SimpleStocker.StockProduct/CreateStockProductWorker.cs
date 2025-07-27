using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SimpleStocker.StockConsumer.Models;
using SimpleStocker.StockConsumer.Repositories;
using System.Text;
using System.Text.Json;

namespace SimpleStocker.StockConsumer
{
    public class CreateStockProductWorker : BackgroundService
    {
        private readonly ILogger<CreateStockProductWorker> _logger;
        private IConnection _connection;
        private IModel _channel;
        private IServiceProvider _provider;
        private const string ExchangeName = "DirectProductSender";
        private const string QueueName = "CreateProductQueue";

        public CreateStockProductWorker(ILogger<CreateStockProductWorker> logger, IServiceProvider provider)
        {
            _provider = provider;
            _logger = logger;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);

            _channel.QueueDeclare(queue: QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {


            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, evt) =>
            {
                var message = Encoding.UTF8.GetString(evt.Body.ToArray());

                _logger.LogInformation($"Mensagem recebida do tópico: {message}");
                var product = JsonSerializer.Deserialize<ProductModelEvent>(message);
                using var scope = _provider.CreateScope();
                var stockRepository = scope.ServiceProvider.GetRequiredService<IStockRepository>();

                stockRepository.CreateProduct(product).GetAwaiter().GetResult();
                _logger.LogInformation($"Inventory criado para ProductId: {product.Id}");
            };
            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
