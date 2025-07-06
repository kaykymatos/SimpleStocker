using SimpleStocker.ClientApi.Tests.Builder;
using SimpleStocker.ClientApi.Validations;

namespace SimpleStocker.ClientApi.Tests;

public class ClientTests
{
    private readonly ClientModelBuilder _builder;
    private readonly ClientValidator _validator;

    public ClientTests()
    {
        _builder = new ClientModelBuilder();
        _validator = new ClientValidator();
    }

    [Fact]
    public void Should_Validate_Valid_Client()
    {
        var client = _builder.Build();
        var result = _validator.Validate(client);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")]
    [InlineData("AB")]
    public void Should_Fail_When_Name_Is_Invalid(string name)
    {
        var client = _builder.With(x => x.Name = name).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public void Should_Fail_When_Name_Too_Long()
    {
        var client = _builder.With(x => x.Name = new string('A', 101)).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("invalid-email")]
    public void Should_Fail_When_Email_Is_Invalid(string email)
    {
        var client = _builder.With(x => x.Email = email).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("123456789")]
    [InlineData("1234567890123456")]
    public void Should_Fail_When_PhoneNumer_Is_Invalid(string phone)
    {
        var client = _builder.With(x => x.PhoneNumer = phone).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "PhoneNumer");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("AB")]
    public void Should_Fail_When_Address_Is_Invalid(string address)
    {
        var client = _builder.With(x => x.Address = address).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "Address");
    }

    [Fact]
    public void Should_Fail_When_Address_Too_Long()
    {
        var client = _builder.With(x => x.Address = new string('A', 101)).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "Address");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("12345678901")]
    public void Should_Fail_When_AddressNumber_Is_Invalid(string addressNumber)
    {
        var client = _builder.With(x => x.AddressNumber = addressNumber).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "AddressNumber");
    }

    [Fact]
    public void Should_Fail_When_BirthDate_Is_Empty()
    {
        var client = _builder.With(x => x.BirthDate = default).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "BirthDate");
    }

    [Fact]
    public void Should_Fail_When_BirthDate_Is_Future()
    {
        var client = _builder.With(x => x.BirthDate = DateTime.UtcNow.AddDays(1)).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "BirthDate");
    }

    [Fact]
    public void Should_Fail_When_BirthDate_Is_Too_Old()
    {
        var client = _builder.With(x => x.BirthDate = new DateTime(1899, 12, 31)).Build();
        var result = _validator.Validate(client);
        Assert.Contains(result.Errors, e => e.PropertyName == "BirthDate");
    }
}
