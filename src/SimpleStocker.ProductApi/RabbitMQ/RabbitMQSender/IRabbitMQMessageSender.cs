using SimpleStocker.MessageBus;

namespace SimpleStocker.ProductApi.RabbitMQ.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
