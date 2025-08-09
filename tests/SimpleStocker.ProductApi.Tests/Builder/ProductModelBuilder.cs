using FizzWare.NBuilder;
using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.ProductApi.Tests.Builder
{
    public class ProductModelBuilder : BuilderBase<ProductDTO>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<ProductDTO>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.Name = "Produto Teste")
                .With(x => x.Description = "Descrição do produto")
                .With(x => x.QuantityStock = 10)
                .With(x => x.UnityOfMeasurement = "kg")
                .With(x => x.Price = 100.0m)
                .With(x => x.CategoryId = 1)
                .With(x => x.CategoryName = "Categoria Teste")
                .With(x => x.CreatedDate = DateTime.UtcNow)
                .With(x => x.UpdatedDate = DateTime.UtcNow);
        }
    }
}