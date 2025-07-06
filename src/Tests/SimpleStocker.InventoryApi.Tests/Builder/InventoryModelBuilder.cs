using FizzWare.NBuilder;
using SimpleStocker.InventoryApi.DTO;

namespace SimpleStocker.InventoryApi.Tests.Builder
{
    public class InventoryModelBuilder : BuilderBase<InventoryDTO>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<InventoryDTO>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.ProductId = 1)
                .With(x => x.Quantity = 100);
        }
    }
}