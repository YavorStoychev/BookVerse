using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookVerse.GCommon.ValidationConstants.Book;

namespace BookVerse.DataModels
{
    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(IsbnMax)]
        public string Isbn { get; set; } = null!;


        [Required, StringLength(TitleMax)]
        public string Title { get; set; } = null!;


        [Required, StringLength(DescriptionMax)]
        public string Description { get; set; } = null!;


        [MaxLength(CoverImageUrlMax)]
        public string? CoverImageUrl { get; set; }


        [Required]
        public  DateOnly PublishedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        
        public string PublisherId { get; set; } = null!;

        public virtual IdentityUser Publisher { get; set; } = null!;

        [Required]
        
        public virtual int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;


        public virtual ICollection<UserBook> UsersBooks { get; set; } 
                = new HashSet<UserBook>();
    }
}
