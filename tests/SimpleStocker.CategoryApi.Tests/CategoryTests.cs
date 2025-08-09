using SimpleStocker.CategoryApi.Tests.Builder;
using SimpleStocker.ProductApi.Validations;

namespace SimpleStocker.CategoryApi.Tests
{
    public class CategoryTests
    {
        private readonly CategoryModelBuilder _builder = new();
        private readonly CategoryValidator _validator = new();

        [Fact]
        public void Should_Validate_Valid_Category()
        {
            var category = _builder.Build();
            var result = _validator.Validate(category);
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("A")]
        [InlineData("AB")]
        public void Should_Fail_When_Name_Is_Invalid(string name)
        {
            var category = _builder.With(x => x.Name = name).Build();
            var result = _validator.Validate(category);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Should_Fail_When_Name_Too_Long()
        {
            var category = _builder.With(x => x.Name = new string('A', 101)).Build();
            var result = _validator.Validate(category);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("AB")]
        public void Should_Fail_When_Description_Is_Invalid(string desc)
        {
            var category = _builder.With(x => x.Description = desc).Build();
            var result = _validator.Validate(category);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description");
        }

        [Fact]
        public void Should_Fail_When_Description_Too_Long()
        {
            var category = _builder.With(x => x.Description = new string('A', 256)).Build();
            var result = _validator.Validate(category);
            Assert.Contains(result.Errors, e => e.PropertyName == "Description");
        }
    }
}
