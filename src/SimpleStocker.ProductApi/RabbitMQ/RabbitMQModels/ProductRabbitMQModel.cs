using SimpleStocker.MessageBus;

namespace SimpleStocker.ProductApi.RabbitMQ.RabbitMQModels
{
    public class ProductRabbitMQModel : BaseMessage
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double QuantityStock { get; set; } = 0;
        public string UnityOfMeasurement { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public long CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
    }
}
