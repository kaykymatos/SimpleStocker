using Microsoft.Extensions.DependencyInjection;
using SimpleStocker.Api.Validations;
using SimpleStocker.Tests.Builder;

namespace SimpleStocker.Tests.ValidationTests
{
    public class ClientViewModelTests
    {
        private readonly ClientViewModelBuilder _builder;
        private readonly ClientValidator _validator;

        public ClientViewModelTests()
        {
            var provider = new ServiceCollection()
                .AddScoped<ClientValidator>()
                .BuildServiceProvider();

            _builder = new ClientViewModelBuilder();
            _validator = new ClientValidator(); // se quiser testar update=true, altere aqui
        }

        [Fact(DisplayName = "Cliente válido")]
        public async Task DeveSerValido()
        {
            var instance = _builder.Build();
            var result = await _validator.ValidateAsync(instance);
            Assert.True(result.IsValid);
        }
        public static IEnumerable<object[]> NomesInvalidos =>
        [
            ["", "O nome é obrigatório."],
            ["AB", "O nome deve ter no mínimo 3 caracteres."],
            [new string('A', 101), "O nome deve ter no máximo 100 caracteres."]
        ];

        [Theory(DisplayName = "Nome inválido")]
        [MemberData(nameof(NomesInvalidos))]
        public async Task DeveSerInvalido_QuandoNomeInvalido(string nome, string mensagemEsperada)
        {
            var instance = _builder.Build();
            instance.Name = nome;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains(mensagemEsperada));
        }

        [Theory(DisplayName = "Email inválido")]
        [InlineData("", "obrigatório")]
        [InlineData("invalido@", "válido")]
        public async Task DeveSerInvalido_QuandoEmailInvalido(string email, string mensagemEsperada)
        {
            var instance = _builder.Build();
            instance.Email = email;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email" && e.ErrorMessage.Contains(mensagemEsperada));
        }

        [Theory(DisplayName = "Telefone inválido")]
        [InlineData("", "obrigatório")]
        [InlineData("12345", "mínimo")]
        [InlineData("1234567890123456", "máximo")]
        public async Task DeveSerInvalido_QuandoTelefoneInvalido(string telefone, string mensagemEsperada)
        {
            var instance = _builder.Build();
            instance.PhoneNumer = telefone;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "PhoneNumer" && e.ErrorMessage.Contains(mensagemEsperada));
        }
        public static IEnumerable<object[]> EnderecosInvalidos =>
            [
                ["", "O endereço é obrigatório."],
                ["AB", "O endereço deve ter no mínimo 3 caracteres."],
                ["Rua " + new string('X', 100), "O endereço deve ter no máximo 100 caracteres."]
            ];

        [Theory(DisplayName = "Endereço inválido")]
        [MemberData(nameof(EnderecosInvalidos))]
        public async Task DeveSerInvalido_QuandoEnderecoInvalido(string endereco, string mensagemEsperada)
        {
            var instance = _builder.Build();
            instance.Address = endereco;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Address" && e.ErrorMessage.Contains(mensagemEsperada));
        }

        [Theory(DisplayName = "Número do endereço inválido")]
        [InlineData("", "obrigatório")]
        [InlineData("12345678901", "máximo")]
        public async Task DeveSerInvalido_QuandoEnderecoNumeroInvalido(string numero, string mensagemEsperada)
        {
            var instance = _builder.Build();
            instance.AddressNumber = numero;

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "AddressNumber" && e.ErrorMessage.Contains(mensagemEsperada));
        }

        [Theory(DisplayName = "Data de nascimento inválida")]
        [InlineData("1899-12-31", "maior que 01/01/1900")]
        [InlineData("2999-01-01", "anterior à data atual")]
        public async Task DeveSerInvalido_QuandoDataNascimentoInvalida(string data, string mensagemEsperada)
        {
            var instance = _builder.Build();
            instance.BirthDate = DateTime.Parse(data);

            var result = await _validator.ValidateAsync(instance);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "BirthDate" && e.ErrorMessage.Contains(mensagemEsperada));
        }
    }
}
