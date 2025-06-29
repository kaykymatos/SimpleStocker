using FluentValidation;
using SimpleStocker.InventoryApi.DTO;

namespace SimpleStocker.InventoryApi.Validations
{
    public class InventoryValidator : AbstractValidator<InventoryDTO>
    {
        public InventoryValidator(bool update = false)
        {

        }
    }
}
