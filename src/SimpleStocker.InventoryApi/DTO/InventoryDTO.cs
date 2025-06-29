namespace SimpleStocker.InventoryApi.DTO
{
    public class InventoryDTO
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
