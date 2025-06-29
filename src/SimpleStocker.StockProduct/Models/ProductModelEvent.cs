namespace SimpleStocker.StockConsumer.Models
{
    public class ProductModelEvent
    {
        public long Id { get; set; }
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
