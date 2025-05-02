namespace SimpleStocker.Api.Models.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double QuantityStock { get; set; }
        public string UnityOfMeasurement { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
    }
}
