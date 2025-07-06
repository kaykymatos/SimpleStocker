using FizzWare.NBuilder;
using SimpleStocker.SaleApi.DTO;

namespace SimpleStocker.SaleApi.Tests.Builder
{
    public class SaleItemModelBuilder : BuilderBase<SaleItemDTO>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<SaleItemDTO>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.ProductId = 1)
                .With(x => x.SaleId = 1)
                .With(x => x.Quantity = 1)
                .With(x => x.UnityPrice = 10)
                .With(x => x.CreatedDate = DateTime.UtcNow)
                .With(x => x.UpdatedDate = DateTime.UtcNow);
        }
    }
}