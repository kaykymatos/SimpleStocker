using Confluent.Kafka;
using System.Text.Json;

namespace SimpleStocker.ProductApi.Producers
{
    public class BasicProducer
    {
        public static async Task ProduceMessage(string topicMessage, CancellationToken token, object model)
        {
            var config = Config.KafkaConfig.ConfigProducer(topicMessage);

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                var deliveryResult = await producer.ProduceAsync(topic: topicMessage,
                    new Message<Null, string>
                    {
                        Value = JsonSerializer.Serialize(model)
                    }, token);
            }
            catch (ProduceException<Null, string> e)
            {
                throw new Exception(e.Message);
            }

            producer.Flush(token);
        }
    }
}
