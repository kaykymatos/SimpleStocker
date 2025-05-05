using FluentValidation;
using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator()
        {

        }
    }
}
