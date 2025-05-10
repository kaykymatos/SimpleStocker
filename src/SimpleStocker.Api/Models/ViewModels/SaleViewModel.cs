using SimpleStocker.Api.Models.Entities.Enums;

namespace SimpleStocker.Api.Models.ViewModels
{
    public class SaleViewModel : BaseViewModel
    {
        public List<SaleItemViewModel> Items { get; set; } = [];
        public decimal TotalAmount { get => Items.Sum(item => item.SubTotal) - Discount; }
        public long CustomerId { get; set; } = 0;
        public decimal Discount { get; private set; } = 0;
        public EPaymentMethod PaymentMethod { get; set; } = EPaymentMethod.Pix;
        public ESaleStatus Status { get; set; } = ESaleStatus.Pending;
        public SaleViewModel()
        {
            
        }

        public SaleViewModel(long id, DateTime creationDate, DateTime updatedDate, long customerId, decimal discount, EPaymentMethod paymentMethod, ESaleStatus status, List<SaleItemViewModel> items) : base(id, creationDate, updatedDate)
        {
            CustomerId = customerId;
            Discount = discount;
            PaymentMethod = paymentMethod;
            Status = status;
            Items = items;
        }
    }
}
