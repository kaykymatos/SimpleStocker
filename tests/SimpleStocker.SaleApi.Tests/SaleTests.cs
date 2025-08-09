using SimpleStocker.SaleApi.Models.Enums;
using SimpleStocker.SaleApi.Tests.Builder;
using SimpleStocker.SaleApi.Validations;

namespace SimpleStocker.SaleApi.Tests
{
    public class SaleTests
    {
        private readonly SaleModelBuilder _builder = new();
        private readonly SaleValidator _validator = new();

        [Fact]
        public void Should_Validate_Valid_Sale()
        {
            var sale = _builder.Build();
            var result = _validator.Validate(sale);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Fail_When_Items_Is_Null()
        {
            var sale = _builder.With(x => x.Items = null).Build();
            var result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "Items");
        }

        [Fact]
        public void Should_Fail_When_Items_Is_Empty()
        {
            var sale = _builder.With(x => x.Items = new List<SimpleStocker.SaleApi.DTO.SaleItemDTO>()).Build();
            var result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "Items");
        }

        [Fact]
        public void Should_Fail_When_TotalAmount_Is_Zero_Or_Negative()
        {
            var sale = _builder.With(x => x.TotalAmount = 0).Build();
            var result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "TotalAmount");
            sale = _builder.With(x => x.TotalAmount = -1).Build();
            result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "TotalAmount");
        }

        [Fact]
        public void Should_Fail_When_ClientId_Is_Zero_Or_Negative()
        {
            var sale = _builder.With(x => x.ClientId = 0).Build();
            var result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "ClientId");
            sale = _builder.With(x => x.ClientId = -1).Build();
            result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "ClientId");
        }

        [Fact]
        public void Should_Fail_When_Discount_Is_Negative()
        {
            var sale = _builder.With(x => x.Discount = -1).Build();
            var result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "Discount");
        }

        [Fact]
        public void Should_Fail_When_PaymentMethod_Is_Invalid()
        {
            var sale = _builder.With(x => x.PaymentMethod = (EPaymentMethod)999).Build();
            var result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "PaymentMethod");
        }

        [Fact]
        public void Should_Fail_When_Status_Is_Invalid()
        {
            var sale = _builder.With(x => x.Status = (ESaleStatus)999).Build();
            var result = _validator.Validate(sale);
            Assert.Contains(result.Errors, e => e.PropertyName == "Status");
        }
    }
}
