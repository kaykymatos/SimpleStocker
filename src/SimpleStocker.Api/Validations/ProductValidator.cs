using FluentValidation;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Validations
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {

        }
    }
}
