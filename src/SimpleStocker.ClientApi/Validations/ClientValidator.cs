using FluentValidation;
using SimpleStocker.ClientApi.DTO;

namespace SimpleStocker.ClientApi.Validations
{
    public class ClientValidator : AbstractValidator<ClientDTO>
    {
        public ClientValidator(bool update = false)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(x => x.PhoneNumer)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O número de telefone é obrigatório.")
                .MinimumLength(10).WithMessage("O número de telefone deve ter no mínimo 10 dígitos.")
                .MaximumLength(15).WithMessage("O número de telefone deve ter no máximo 15 dígitos.");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .MinimumLength(3).WithMessage("O endereço deve ter no mínimo 3 caracteres.")
                .MaximumLength(100).WithMessage("O endereço deve ter no máximo 100 caracteres.");

            RuleFor(x => x.AddressNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O número do endereço é obrigatório.")
                .MaximumLength(10).WithMessage("O número do endereço deve ter no máximo 10 caracteres.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser anterior à data atual.")
                .GreaterThan(new DateTime(1900, 1, 1)).WithMessage("A data de nascimento deve ser maior que 01/01/1900.");

        }
    }
}
