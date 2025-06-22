using FluentValidation;
using SimpleStocker.SaleApi.DTO;

namespace SimpleStocker.SaleApi.Validations
{
    public class SaleItemValidator : AbstractValidator<SaleItemDTO>
    {
        public SaleItemValidator(bool update = false)
        {

        }
    }
}
