using SimpleStocker.Api.Models.Entities.Enums;

namespace SimpleStocker.Api.Models.Entities
{
    public class Sale : BaseEntity
    {
        public List<SaleItem> Items { get; set; }
        public long CustomerId { get; set; }
        public decimal TotalAmount { get => Items.Sum(item => item.SubTotal) - Discount; }
        public decimal Discount { get; private set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public ESaleStatus Status { get; set; }


    }
}
