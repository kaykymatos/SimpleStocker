namespace SimpleStocker.Api.Models.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double QuantityStock { get; set; } = 0;
        public string UnityOfMeasurement { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public long CategoryId { get; set; } = 0;
        public ProductViewModel()
        {

        }

        public ProductViewModel(long id, DateTime creationDate, DateTime updatedDate, string name, string description, double quantityStock, string unityOfMeasurement, decimal price, long categoryId) : base(id, creationDate, updatedDate)
        {
            Name = name;
            Description = description;
            QuantityStock = quantityStock;
            UnityOfMeasurement = unityOfMeasurement;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
