using SimpleStocker.MessageBus;

namespace SimpleStocker.SaleApi.RabbitMQ.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
