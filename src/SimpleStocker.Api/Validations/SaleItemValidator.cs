using FluentValidation;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Validations
{
    public class SaleItemValidator : AbstractValidator<SaleItemViewModel>
    {
        public SaleItemValidator(bool update = false)
        {

        }
    }
}
