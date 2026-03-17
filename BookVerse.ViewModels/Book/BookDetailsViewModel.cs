namespace BookVerse.ViewModels.Book
{
    public class BookDetailsViewModel : BookIndexViewModel
    {
        public string Description { get; set; } = null!;
        public string PublishedOn { get; set; } = null!;
        public string Publisher { get; set; } = null!;
    }
}
