using SimpleStocker.StockConsumer.Models;

namespace SimpleStocker.StockConsumer.Repositories
{
    public interface IStockRepository
    {
        Task UpdateStock(List<SaleItemConsumerEventModel> items);
        Task CreateProduct(ProductModelEvent product);
        Task UpdateProduct(ProductModelEvent product);
    }
}
