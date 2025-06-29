namespace SimpleStocker.InventoryApi.Models
{
    public class InventoryModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }

    }
}
