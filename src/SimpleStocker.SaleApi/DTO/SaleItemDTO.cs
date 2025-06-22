namespace SimpleStocker.SaleApi.DTO
{
    public class SaleItemDTO
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long SaleId { get; set; } = 0;
        public long ProductId { get; set; } = 0;
        public double Quantity { get; set; } = 0;
        public decimal UnityPrice { get; set; } = 0;
        public decimal SubTotal { get => (decimal)Quantity * UnityPrice; }

    }
}
