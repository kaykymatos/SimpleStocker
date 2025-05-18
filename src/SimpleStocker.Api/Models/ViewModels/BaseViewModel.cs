namespace SimpleStocker.Api.Models.ViewModels
{
    public class BaseViewModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public BaseViewModel()
        {

        }

        public BaseViewModel(long id, DateTime createdDate, DateTime updatedDate)
        {
            Id = id;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
    }
}
