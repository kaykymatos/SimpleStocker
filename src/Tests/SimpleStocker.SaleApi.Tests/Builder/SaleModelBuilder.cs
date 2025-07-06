using FizzWare.NBuilder;
using SimpleStocker.SaleApi.DTO;
using SimpleStocker.SaleApi.Models.Enums;

namespace SimpleStocker.SaleApi.Tests.Builder
{
    public class SaleModelBuilder : BuilderBase<SaleDTO>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<SaleDTO>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.CreatedDate = DateTime.UtcNow)
                .With(x => x.UpdatedDate = DateTime.UtcNow)
                .With(x => x.Items = new List<SaleItemDTO> { new SaleItemDTO { Id = 1, ProductId = 1, Quantity = 1, UnityPrice = 10, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, SaleId = 1 } })
                .With(x => x.TotalAmount = 10)
                .With(x => x.ClientId = 1)
                .With(x => x.Discount = 0)
                .With(x => x.PaymentMethod = EPaymentMethod.Pix)
                .With(x => x.Status = ESaleStatus.Pending);
        }
    }
}