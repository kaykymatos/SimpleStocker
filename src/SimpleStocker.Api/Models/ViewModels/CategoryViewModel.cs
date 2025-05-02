namespace SimpleStocker.Api.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
