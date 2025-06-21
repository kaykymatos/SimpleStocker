using FluentValidation;
using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.ProductApi.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.")
                .MinimumLength(3).WithMessage("A descrição deve ter no mínimo 3 caracteres.");
        }
    }
}
