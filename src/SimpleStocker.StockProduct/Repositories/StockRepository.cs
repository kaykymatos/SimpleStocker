using Microsoft.EntityFrameworkCore;
using SimpleStocker.StockConsumer.Context;
using SimpleStocker.StockConsumer.Models;

namespace SimpleStocker.StockConsumer.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApiContext _context;
        public StockRepository(ApiContext context)
        {
            _context = context;
        }
        public async Task CreateProduct(ProductModelEvent product)
        {
            _context.Inventory.Add(new InventoryEventModel { ProductId = product.Id, Quantity = product.QuantityStock });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(ProductModelEvent product)
        {
            InventoryEventModel productFound = null;
            int count = 0;
            do
            {
                count++;
                productFound = _context.Inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productFound != null)
                {
                    productFound.Quantity = product.QuantityStock;
                    _context.Inventory.Update(productFound);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await Task.Delay(1000);
                }
            } while (productFound == null && count < 5);
        }

        public async Task UpdateStock(List<SaleItemConsumerEventModel> items)
        {
            // Agrupa os itens por ProductId e soma as quantidades
            var groupedItems = items
                .GroupBy(x => x.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                });

            List<InventoryEventModel> updatedProducts = [];

            foreach (var item in groupedItems)
            {
                updatedProducts.Add(new InventoryEventModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            var models = await _context.Inventory
                .Where(x => updatedProducts
                    .Select(x => x.ProductId)
                    .Contains(x.ProductId))
                .ToListAsync();

            foreach (var item in models)
                item.Quantity -= updatedProducts
                    .Find(x => x.ProductId == item.ProductId)
                    .Quantity;

            _context.Inventory.UpdateRange(models);
            await _context.SaveChangesAsync();

        }
    }
}
