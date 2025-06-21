using System.Text.Json.Serialization;

namespace SimpleStocker.ProductApi.Models
{
    public class CategoryModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
