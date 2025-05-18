namespace SimpleStocker.Api.Models.ViewModels
{
    public class SaleItemViewModel : BaseViewModel
    {
        public long SaleId { get; set; } = 0;
        public long ProductId { get; set; } = 0;
        public double Quantity { get; set; } = 0;
        public decimal UnityPrice { get; set; } = 0;
        public decimal SubTotal { get => (decimal)Quantity * UnityPrice; }
        public SaleItemViewModel()
        {

        }

        public SaleItemViewModel(long id, DateTime creationDate, DateTime updatedDate, long saleId, long productId, double quantity, decimal unityPrice) : base(id, creationDate, updatedDate)
        {
            SaleId = saleId;
            ProductId = productId;
            Quantity = quantity;
            UnityPrice = unityPrice;
        }
    }
}
