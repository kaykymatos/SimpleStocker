using FizzWare.NBuilder;
using SimpleStocker.ClientApi.DTO;

namespace SimpleStocker.ClientApi.Tests.Builder
{
    public class ClientModelBuilder : BuilderBase<ClientDTO>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<ClientDTO>.CreateNew()
                .With(x => x.Name = "Cliente Teste")
                .With(x => x.Email = "cliente@teste.com")
                .With(x => x.PhoneNumer = "11999999999")
                .With(x => x.Address = "Rua Teste")
                .With(x => x.AddressNumber = "123")
                .With(x => x.Active = true)
                .With(x => x.BirthDate = DateTime.UtcNow.AddYears(-30))
                .With(x => x.CreatedDate = DateTime.UtcNow)
                .With(x => x.UpdatedDate = DateTime.UtcNow)
                .With(x => x.Id = 1);
        }
    }
}