namespace SimpleStocker.Api.Models.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get => DateTime.Now; }
        public DateTime UpdatedDate { get; set; }
    }
}
