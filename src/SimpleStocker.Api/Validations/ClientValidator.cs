using FluentValidation;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Validations
{
    public class ClientValidator : AbstractValidator<ClientViewModel>
    {
        public ClientValidator(bool update = false)
        {

        }
    }
}
