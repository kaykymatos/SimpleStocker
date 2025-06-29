using Confluent.Kafka;

namespace SimpleStocker.NotificationEmailConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "email-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe("email-sender");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var createResult = consumer.Consume(TimeSpan.FromMilliseconds(500));
                    if (createResult != null)
                    {
                        _logger.LogInformation($"Mensagem recebida do tópico: {createResult.Topic}");

                        _logger.LogInformation($"Email enviado: {createResult.Message.Value}");
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
