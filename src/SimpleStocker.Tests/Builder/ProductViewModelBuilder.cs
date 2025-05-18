using FizzWare.NBuilder;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Tests.Builder
{
    public class ProductViewModelBuilder : BuilderBase<ProductViewModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<ProductViewModel>.CreateNew()
                 .With(x => x.Name = "Arroz Tipo 1")
                 .With(x => x.Description = "Pacote de 5kg de arroz tipo 1")
                 .With(x => x.QuantityStock = 150)
                 .With(x => x.UnityOfMeasurement = "kg")
                 .With(x => x.Price = 24.90m)
                 .With(x => x.CategoryId = 1)
                 .With(x => x.Id = 1)
                 .With(x => x.CreatedDate = DateTime.UtcNow)
                 .With(x => x.UpdatedDate = DateTime.UtcNow);
        }
    }
}
