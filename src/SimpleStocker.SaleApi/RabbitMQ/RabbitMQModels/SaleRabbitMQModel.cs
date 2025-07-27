using SimpleStocker.MessageBus;
using SimpleStocker.SaleApi.DTO;
using SimpleStocker.SaleApi.Models.Enums;

namespace SimpleStocker.SaleApi.RabbitMQ.RabbitMQModels
{
    public class SaleRabbitMQModel : BaseMessage
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<SaleItemDTO> Items { get; set; } = [];
        public decimal TotalAmount { get; set; }
        public long ClientId { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public EPaymentMethod PaymentMethod { get; set; } = EPaymentMethod.Pix;
        public ESaleStatus Status { get; set; } = ESaleStatus.Pending;
    }
}
