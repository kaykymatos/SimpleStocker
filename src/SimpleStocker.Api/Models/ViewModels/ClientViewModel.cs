namespace SimpleStocker.Api.Models.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumer { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public bool Active { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
