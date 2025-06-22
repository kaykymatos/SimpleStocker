using FluentValidation;
using SimpleStocker.SaleApi.DTO;

namespace SimpleStocker.SaleApi.Validations
{
    public class SaleValidator : AbstractValidator<SaleDTO>
    {
        public SaleValidator(bool update = false)
        {
            RuleFor(x => x.Items)
            .Cascade(CascadeMode.Stop)
           .NotNull().WithMessage("A lista de itens não pode ser nula.")
           .Must(items => items.Any()).WithMessage("A venda deve conter pelo menos um item.");

            RuleFor(x => x.TotalAmount)
            .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("O valor total da venda deve ser maior que zero.");

            RuleFor(x => x.ClientId)
            .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Cliente inválido.");

            RuleFor(x => x.Discount)
            .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0).WithMessage("O desconto não pode ser negativo.");

            RuleFor(x => x.PaymentMethod)
            .Cascade(CascadeMode.Stop)
                .IsInEnum().WithMessage("Método de pagamento inválido.");

            RuleFor(x => x.Status)
            .Cascade(CascadeMode.Stop)
                .IsInEnum().WithMessage("Status da venda inválido.");
        }
    }
}
