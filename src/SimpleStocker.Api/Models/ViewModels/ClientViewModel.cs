namespace SimpleStocker.Api.Models.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumer { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AddressNumber { get; set; } = string.Empty;
        public bool Active { get; set; } = false;
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        public ClientViewModel()
        {
            
        }

        public ClientViewModel(long id, DateTime creationDate, DateTime updatedDate, string name, string email, string phoneNumer, string address, string addressNumber, bool active, DateTime birthDate) : base(id, creationDate, updatedDate)
        {
            Name = name;
            Email = email;
            PhoneNumer = phoneNumer;
            Address = address;
            AddressNumber = addressNumber;
            Active = active;
            BirthDate = birthDate;
        }
    }
}
