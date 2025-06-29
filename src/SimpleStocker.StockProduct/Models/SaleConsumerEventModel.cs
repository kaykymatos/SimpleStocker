namespace SimpleStocker.StockConsumer.Models
{
    public class SaleConsumerEventModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<SaleItemConsumerEventModel> Items { get; set; } = [];
        public decimal TotalAmount { get; set; }
        public long ClientId { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public long PaymentMethod { get; set; }
        public long Status { get; set; }
    }
    public class SaleItemConsumerEventModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long SaleId { get; set; } = 0;
        public long ProductId { get; set; } = 0;
        public double Quantity { get; set; } = 0;
        public decimal UnityPrice { get; set; } = 0;
        public decimal SubTotal { get; set; }

    }
}
