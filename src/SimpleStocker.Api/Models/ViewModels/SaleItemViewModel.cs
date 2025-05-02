namespace SimpleStocker.Api.Models.ViewModels
{
    public class SaleItemViewModel : BaseViewModel
    {
        public long SaleId { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal UnityPrice{ get; set; }
        public decimal SubTotal { get => (decimal)Quantity * UnityPrice; }
    }
}
