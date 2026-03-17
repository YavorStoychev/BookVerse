namespace BookVerse.ViewModels.Book
{
    using BookVerse.ViewModels.Genre;
    using System.ComponentModel.DataAnnotations;
    using static GCommon.ValidationConstants.Book;
    public class BookFormModel
    {
        [Required]
        [StringLength(IsbnMax, MinimumLength = IsbnMin)]
        public string Isbn { get; set; } = null!;

        [Required]
        [StringLength(TitleMax, MinimumLength = TitleMin)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMax, MinimumLength = DescriptionMin)]
        public string Description { get; set; } = null!;

        [MaxLength(CoverImageUrlMax)]
        [Url]
        public string? CoverImageUrl { get; set; }

        public DateTime PublishedOn { get; set; }

        public int GenreId { get; set; }

        public virtual IEnumerable<GenreViewModel> Genres { get; set; }
                = new List<GenreViewModel>();
    }
}
