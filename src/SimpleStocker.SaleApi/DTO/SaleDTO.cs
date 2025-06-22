using SimpleStocker.SaleApi.Models.Entities.Enums;

namespace SimpleStocker.SaleApi.DTO
{
    public class SaleDTO
    {
        public long Id { get; set; }
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
