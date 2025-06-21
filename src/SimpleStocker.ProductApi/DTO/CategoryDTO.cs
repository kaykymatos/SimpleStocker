namespace SimpleStocker.ProductApi.DTO
{
    public class CategoryDTO
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
