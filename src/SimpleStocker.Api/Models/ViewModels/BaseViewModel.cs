namespace SimpleStocker.Api.Models.ViewModels
{
    public class BaseViewModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get => DateTime.Now; }
        public DateTime UpdatedDate { get; set; }
    }
}
