using Microsoft.Extensions.DependencyInjection;
using SimpleStocker.Api.Validations;
using SimpleStocker.Tests.Builder;

namespace SimpleStocker.Tests.ValidationTests
{
    public class CategoryViewModelTests
    {
        private readonly CategoryViewModelBuilder _builder;
        private readonly CategoryValidator _validator;

        public CategoryViewModelTests()
        {
            var provider = new ServiceCollection()
                .AddScoped<CategoryValidator>()
                .BuildServiceProvider();

            _builder = new CategoryViewModelBuilder();
            _validator = provider.GetService<CategoryValidator>()!;
        }

        [Fact(DisplayName = "Categoria válida")]
        public async Task DeveSerValido()
        {
            var instance = _builder.Build();
            var result = await _validator.ValidateAsync(instance);
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Nome vazio")]
        public async Task DeveSerInvalido_QuandoNomeVazio()
        {
            var instance = _builder.Build();
            instance.Name = "";

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Fact(DisplayName = "Nome muito curto")]
        public async Task DeveSerInvalido_QuandoNomeMuitoCurto()
        {
            var instance = _builder.Build();
            instance.Name = "AB";

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("mínimo"));
        }

        [Fact(DisplayName = "Nome muito longo")]
        public async Task DeveSerInvalido_QuandoNomeMuitoLongo()
        {
            var instance = _builder.Build();
            instance.Name = new string('A', 101);

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("máximo"));
        }

        [Fact(DisplayName = "Descrição vazia")]
        public async Task DeveSerInvalido_QuandoDescricaoVazia()
        {
            var instance = _builder.Build();
            instance.Description = "";

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains("obrigatória"));
        }

        [Fact(DisplayName = "Descrição muito curta")]
        public async Task DeveSerInvalido_QuandoDescricaoMuitoCurta()
        {
            var instance = _builder.Build();
            instance.Description = "AB";

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains("mínimo"));
        }

        [Fact(DisplayName = "Descrição muito longa")]
        public async Task DeveSerInvalido_QuandoDescricaoMuitoLonga()
        {
            var instance = _builder.Build();
            instance.Description = new string('D', 256);

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains("máximo"));
        }
    }
}
