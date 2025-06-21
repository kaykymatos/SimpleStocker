namespace SimpleStocker.ClientApi.Models
{
    public class ClientModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumer { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AddressNumber { get; set; } = string.Empty;
        public bool Active { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
