using FizzWare.NBuilder;
using SimpleStocker.Api.Models.Entities.Enums;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Tests.Builder
{
    public class SaleViewModelBuilder : BuilderBase<SaleViewModel>
    {
        protected override void LoadDefault()
        {
            var items = new List<SaleItemViewModel>
            {
                new() {
                    ProductId = 1,
                    Quantity = 2,
                    UnityPrice = 10.00m
                },
                new() {
                    ProductId = 2,
                    Quantity = 1,
                    UnityPrice = 15.00m
                }
            };

            _builderInstance = Builder<SaleViewModel>.CreateNew()
                .With(x => x.CustomerId = 123)
                .With(x => x.Discount = 5.00m)
                .With(x => x.PaymentMethod = EPaymentMethod.Pix)
                .With(x => x.Status = ESaleStatus.Pending)
                .With(x => x.Items = items)
                .With(x => x.TotalAmount = items.Sum(i => i.SubTotal) - 5.00m)
                .With(x => x.Id = 1)
                .With(x => x.CreatedDate = DateTime.UtcNow)
                .With(x => x.UpdatedDate = DateTime.UtcNow);
        }
    }
}
