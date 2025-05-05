namespace SimpleStocker.Api.Models.Entities
{
    public class SaleItem : BaseEntity
    {
        public long SaleId { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal SubTotal { get => (decimal)Quantity * UnityPrice; }
    }
}
