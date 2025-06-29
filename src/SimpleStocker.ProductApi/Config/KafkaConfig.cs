using Confluent.Kafka;

namespace SimpleStocker.ProductApi.Config
{
    public class KafkaConfig
    {
        private const string URL = "localhost:9092";
        public static ProducerConfig ConfigProducer(string topic)
        {
            try
            {
                return new ProducerConfig
                {
                    BootstrapServers = URL,
                    AllowAutoCreateTopics = true,
                    Acks = Acks.All
                };
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}
