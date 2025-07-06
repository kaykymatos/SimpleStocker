using FluentValidation;
using SimpleStocker.ProductApi.DTO;

namespace SimpleStocker.ProductApi.Validations
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator(bool update = false)
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

            RuleFor(x => x.UnityOfMeasurement)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("A unidade de medida é obrigatória.")
                .MaximumLength(20).WithMessage("A unidade de medida deve ter no máximo 20 caracteres.")
                .MinimumLength(2).WithMessage("A unidade de medida deve ter no mínimo 2 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("O ID da categoria deve ser maior que zero.");
        }
    }
}
