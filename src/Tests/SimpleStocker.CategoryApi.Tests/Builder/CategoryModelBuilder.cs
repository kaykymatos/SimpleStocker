using FizzWare.NBuilder;
using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.CategoryApi.Tests.Builder
{
    public class CategoryModelBuilder : BuilderBase<CategoryDTO>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<CategoryDTO>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.Name = "Categoria Teste")
                .With(x => x.Description = "Descrição da categoria")
                .With(x => x.CreatedDate = DateTime.UtcNow)
                .With(x => x.UpdatedDate = DateTime.UtcNow);
        }
    }
}