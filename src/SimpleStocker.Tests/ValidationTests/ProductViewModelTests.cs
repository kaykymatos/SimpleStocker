using Microsoft.Extensions.DependencyInjection;
using SimpleStocker.Api.Validations;
using SimpleStocker.Tests.Builder;

namespace SimpleStocker.Tests.ValidationTests
{
    public class ProductViewModelTests
    {
        private readonly ProductViewModelBuilder _builder;
        private readonly ProductValidator _validator;

        public ProductViewModelTests()
        {
            var provider = new ServiceCollection()
                .AddScoped<ProductValidator>()
                .BuildServiceProvider();

            _builder = new ProductViewModelBuilder();
            _validator = provider.GetService<ProductValidator>();
        }

        [Fact(DisplayName = "Produto válido")]
        public async Task Produto_Valido()
        {
            var instance = _builder.Build();
            var result = await _validator.ValidateAsync(instance);
            Assert.True(result.IsValid);
        }

        public static IEnumerable<object[]> NomesInvalidos =>
            new List<object[]>
            {
                new object[] { "", "O nome é obrigatório." },
                new object[] { "AB", "mínimo" },
                new object[] { new string('A', 101), "máximo" }
            };

        [Theory(DisplayName = "Nome inválido")]
        [MemberData(nameof(NomesInvalidos))]
        public async Task Nome_Invalido(string nome, string mensagem)
        {
            var instance = _builder.Build();
            instance.Name = nome;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains(mensagem));
        }

        public static IEnumerable<object[]> DescricoesInvalidas =>
            new List<object[]>
            {
                new object[] { "", "obrigatória" },
                new object[] { "AB", "mínimo" },
                new object[] { new string('D', 256), "máximo" }
            };

        [Theory(DisplayName = "Descrição inválida")]
        [MemberData(nameof(DescricoesInvalidas))]
        public async Task Descricao_Invalida(string descricao, string mensagem)
        {
            var instance = _builder.Build();
            instance.Description = descricao;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains(mensagem));
        }

        [Theory(DisplayName = "Quantidade inválida")]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Quantidade_Invalida(int quantidade)
        {
            var instance = _builder.Build();
            instance.QuantityStock = quantidade;

            var validator = new ProductValidator(); // sem update
            var result = await validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "QuantityStock");
        }

        public static IEnumerable<object[]> UnidadeMedidaInvalida =>
            new List<object[]>
            {
                new object[] { "", "obrigatória" },
                new object[] { "A", "mínimo" },
                new object[] { new string('U', 21), "máximo" }
            };

        [Theory(DisplayName = "Unidade de medida inválida")]
        [MemberData(nameof(UnidadeMedidaInvalida))]
        public async Task UnidadeMedida_Invalida(string unidade, string mensagem)
        {
            var instance = _builder.Build();
            instance.UnityOfMeasurement = unidade;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "UnityOfMeasurement" && e.ErrorMessage.Contains(mensagem));
        }

        [Theory(DisplayName = "Preço inválido")]
        [InlineData(0)]
        [InlineData(-5.5)]
        public async Task Preco_Invalido(decimal preco)
        {
            var instance = _builder.Build();
            instance.Price = preco;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Price");
        }

        [Theory(DisplayName = "Categoria inválida")]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task CategoriaId_Invalido(long id)
        {
            var instance = _builder.Build();
            instance.CategoryId = id;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CategoryId");
        }
    }
}
