using System.ComponentModel.DataAnnotations;
using static BookVerse.GCommon.ValidationConstants.Genre;

namespace BookVerse.DataModels
{
    public class Genre
    {
        public int Id { get; set; }

        [Required, StringLength(NameMax, MinimumLength = NameMin)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
