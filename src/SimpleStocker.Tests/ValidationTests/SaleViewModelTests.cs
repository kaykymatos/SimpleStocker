using Microsoft.Extensions.DependencyInjection;
using SimpleStocker.Api.Models.Entities.Enums;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Validations;
using SimpleStocker.Tests.Builder;

namespace SimpleStocker.Tests.ValidationTests
{
    public class SaleViewModelTests
    {
        private readonly SaleViewModelBuilder _builder;
        private readonly SaleValidator _validator;

        public SaleViewModelTests()
        {
            var provider = new ServiceCollection()
                .AddScoped<SaleValidator>()
                .BuildServiceProvider();

            _builder = new SaleViewModelBuilder();
            _validator = provider.GetService<SaleValidator>();
        }

        [Fact(DisplayName = "Venda válida")]
        public async Task Venda_Valida()
        {
            var instance = _builder.Build();
            var result = await _validator.ValidateAsync(instance);
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Itens nulos")]
        public async Task Itens_Nulos()
        {
            var instance = _builder.Build();
            instance.Items = null;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Items" && e.ErrorMessage.Contains("nula"));
        }

        [Fact(DisplayName = "Itens vazios")]
        public async Task Itens_Vazios()
        {
            var instance = _builder.Build();
            instance.Items = new List<SaleItemViewModel>();

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Items" && e.ErrorMessage.Contains("pelo menos um item"));
        }

        [Theory(DisplayName = "Valor total inválido")]
        [InlineData(0)]
        [InlineData(-100)]
        public async Task ValorTotal_Invalido(decimal valor)
        {
            var instance = _builder.Build();
            instance.TotalAmount = valor;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "TotalAmount");
        }

        [Theory(DisplayName = "Cliente inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Cliente_Invalido(long clienteId)
        {
            var instance = _builder.Build();
            instance.CustomerId = clienteId;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CustomerId");
        }

        [Theory(DisplayName = "Desconto inválido")]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task Desconto_Invalido(decimal desconto)
        {
            var instance = _builder.Build();
            instance.Discount = desconto;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Discount");
        }

        [Fact(DisplayName = "Método de pagamento inválido")]
        public async Task MetodoPagamento_Invalido()
        {
            var instance = _builder.Build();
            instance.PaymentMethod = (EPaymentMethod)999; // valor fora do enum

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "PaymentMethod");
        }

        [Fact(DisplayName = "Status inválido")]
        public async Task Status_Invalido()
        {
            var instance = _builder.Build();
            instance.Status = (ESaleStatus)999; // valor fora do enum

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Status");
        }
    }
}
