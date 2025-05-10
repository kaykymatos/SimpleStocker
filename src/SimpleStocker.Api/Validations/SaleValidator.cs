using FluentValidation;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Validations
{
    public class SaleValidator : AbstractValidator<SaleViewModel>
    {
        public SaleValidator(bool update = false)
        {

        }
    }
}
