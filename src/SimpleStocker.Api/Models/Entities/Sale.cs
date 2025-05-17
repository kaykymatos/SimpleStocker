using SimpleStocker.Api.Models.Entities.Enums;

namespace SimpleStocker.Api.Models.Entities
{
    public class Sale : BaseEntity
    {
        public List<SaleItem> Items { get; set; }
        public long CustomerId { get; set; }
        public decimal TotalAmount  => Items!=null?Items.Sum(item => item.SubTotal) - Discount:0;
        public decimal Discount { get;  set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public ESaleStatus Status { get; set; }


    }
}
