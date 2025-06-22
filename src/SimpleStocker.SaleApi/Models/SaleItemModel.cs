using SimpleStocker.Api.Models.Entities;

namespace SimpleStocker.SaleApi.Models.Entities
{
    public class SaleItemModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public SaleModel Sale { get; set; }
        public long SaleId { get; set; }
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
