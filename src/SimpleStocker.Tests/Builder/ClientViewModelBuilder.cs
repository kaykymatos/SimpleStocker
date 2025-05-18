using FizzWare.NBuilder;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Tests.Builder
{
    public class ClientViewModelBuilder : BuilderBase<ClientViewModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<ClientViewModel>.CreateNew()
                 .With(x => x.Name = "João da Silva")
                 .With(x => x.Email = "joao.silva@email.com")
                 .With(x => x.PhoneNumer = "(11) 98765-4321")
                 .With(x => x.Address = "Rua das Flores")
                 .With(x => x.AddressNumber = "123")
                 .With(x => x.Active = true)
                 .With(x => x.BirthDate = new DateTime(1990, 5, 15))
                 .With(x => x.Id = 1)
                 .With(x => x.CreatedDate = DateTime.UtcNow)
                 .With(x => x.UpdatedDate = DateTime.UtcNow);
        }
    }
}
