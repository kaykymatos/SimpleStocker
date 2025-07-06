using SimpleStocker.InventoryApi.Tests.Builder;

namespace SimpleStocker.InventoryApi.Tests
{
    public class InventoryTests
    {
        private readonly InventoryModelBuilder _builder = new();

        [Fact]
        public void Should_Build_Valid_Inventory()
        {
            var inventory = _builder.Build();
            Assert.NotNull(inventory);
            Assert.True(inventory.Id > 0);
            Assert.True(inventory.ProductId > 0);
            Assert.True(inventory.Quantity >= 0);
        }

        [Fact]
        public void Should_Allow_Zero_Quantity()
        {
            var inventory = _builder.With(x => x.Quantity = 0).Build();
            Assert.Equal(0, inventory.Quantity);
        }

        [Fact]
        public void Should_Allow_Negative_Quantity()
        {
            var inventory = _builder.With(x => x.Quantity = -10).Build();
            Assert.Equal(-10, inventory.Quantity);
        }
    }
}
