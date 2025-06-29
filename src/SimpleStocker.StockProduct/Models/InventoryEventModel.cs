using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleStocker.StockConsumer.Models
{
    [Table("Inventory")]
    public class InventoryEventModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
