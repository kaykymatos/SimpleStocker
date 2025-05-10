namespace SimpleStocker.Api.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(long id, DateTime creationDate, DateTime updatedDate, string name, string description) : base(id, creationDate, updatedDate)
        {
            Name = name;
            Description = description;
        }
    }
}
