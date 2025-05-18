using FizzWare.NBuilder;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Tests.Builder
{
    public class CategoryViewModelBuilder : BuilderBase<CategoryViewModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<CategoryViewModel>.CreateNew()
                 .With(x => x.Name = "Alimentos")
                 .With(x => x.Description = "Alimentos")
                 .With(x => x.CreatedDate = DateTime.Now);
        }
    }
}
