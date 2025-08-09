using SimpleStocker.ProductApi.Tests.Builder;
using SimpleStocker.ProductApi.Validations;

namespace SimpleStocker.ProductApi.Tests
{
    public class ProductTests
    {
        private readonly ProductModelBuilder _builder = new();
        private readonly ProductValidator _validator = new();

        [Fact]
        public void Should_Validate_Valid_Product()
        {
            var product = _builder.Build();
            var result = _validator.Validate(product);
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("A")]
        [InlineData("AB")]
        public void Should_Fail_When_Name_Is_Invalid(string name)
        {
            var product = _builder.With(x => x.Name = name).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Should_Fail_When_Name_Too_Long()
        {
            var product = _builder.With(x => x.Name = new string('A', 101)).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("AB")]
        public void Should_Fail_When_Description_Is_Invalid(string desc)
        {
            var product = _builder.With(x => x.Description = desc).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description");
        }

        [Fact]
        public void Should_Fail_When_Description_Too_Long()
        {
            var product = _builder.With(x => x.Description = new string('A', 256)).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("A")]
        public void Should_Fail_When_UnityOfMeasurement_Is_Invalid(string unity)
        {
            var product = _builder.With(x => x.UnityOfMeasurement = unity).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "UnityOfMeasurement");
        }

        [Fact]
        public void Should_Fail_When_UnityOfMeasurement_Too_Long()
        {
            var product = _builder.With(x => x.UnityOfMeasurement = new string('A', 21)).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "UnityOfMeasurement");
        }

        [Fact]
        public void Should_Fail_When_Price_Is_Zero_Or_Negative()
        {
            var product = _builder.With(x => x.Price = 0).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "Price");
            product = _builder.With(x => x.Price = -1).Build();
            result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "Price");
        }

        [Fact]
        public void Should_Fail_When_CategoryId_Is_Zero_Or_Negative()
        {
            var product = _builder.With(x => x.CategoryId = 0).Build();
            var result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "CategoryId");
            product = _builder.With(x => x.CategoryId = -1).Build();
            result = _validator.Validate(product);
            Assert.Contains(result.Errors, e => e.PropertyName == "CategoryId");
        }
    }
}

