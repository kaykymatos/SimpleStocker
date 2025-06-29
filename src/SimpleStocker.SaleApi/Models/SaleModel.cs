using SimpleStocker.SaleApi.Models.Enums;

namespace SimpleStocker.SaleApi.Models
{
    public class SaleModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<SaleItemModel> Items { get; set; }
        public long ClientId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public ESaleStatus Status { get; set; }


    }
}
