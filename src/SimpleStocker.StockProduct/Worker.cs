using Confluent.Kafka;
using SimpleStocker.StockConsumer.Models;
using SimpleStocker.StockConsumer.Repositories;
using System.Text.Json;

namespace SimpleStocker.StockConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _provider;

        public Worker(ILogger<Worker> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "stock-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var scope = _provider.CreateScope();
            var stockRepository = scope.ServiceProvider.GetRequiredService<IStockRepository>();

            using var consumerCreate = new ConsumerBuilder<Ignore, string>(config).Build();
            using var consumerUpdate = new ConsumerBuilder<Ignore, string>(config).Build();
            using var consumerSale = new ConsumerBuilder<Ignore, string>(config).Build();

            consumerCreate.Subscribe("create-product-topic");
            consumerUpdate.Subscribe("update-product-topic");
            consumerSale.Subscribe("sale-topic");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var createResult = consumerCreate.Consume(TimeSpan.FromMilliseconds(500));
                    if (createResult != null)
                    {
                        _logger.LogInformation($"Mensagem recebida do tópico: {createResult.Topic}");
                        var product = JsonSerializer.Deserialize<ProductModelEvent>(createResult.Message.Value);
                        await stockRepository.CreateProduct(product);
                        _logger.LogInformation($"Inventory criado para ProductId: {product.Id}");
                    }

                    var updateResult = consumerUpdate.Consume(TimeSpan.FromMilliseconds(500));
                    if (updateResult != null)
                    {
                        _logger.LogInformation($"Mensagem recebida do tópico: {updateResult.Topic}");
                        var productUpdate = JsonSerializer.Deserialize<ProductModelEvent>(updateResult.Message.Value);
                        await stockRepository.UpdateProduct(productUpdate);
                        _logger.LogInformation($"Order processado: {productUpdate.Id}");
                    }

                    var saleResult = consumerSale.Consume(TimeSpan.FromMilliseconds(500));
                    if (saleResult != null)
                    {
                        _logger.LogInformation($"Mensagem recebida do tópico: {saleResult.Topic}");
                        var stockModel = JsonSerializer.Deserialize<SaleConsumerEventModel>(saleResult.Message.Value);
                        if (stockModel != null)
                        {
                            await stockRepository.UpdateStock(stockModel.Items);
                            _logger.LogInformation($"Stock atualizado para: {saleResult.Message.Value}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar mensagem.");
                }
            }
        }
    }
}
