using BookVerse.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.ViewModels.Book
{
    public class BookEditViewModel 
    {
        public int Id { get; set; }

       
        public string Isbn { get; set; } = null!;

       
        public string Title { get; set; } = null!;

      
        public string Description { get; set; } = null!;

       
        public string? CoverImageUrl { get; set; }

        public DateOnly PublishedOn { get; set; }

        public int GenreId { get; set; }

        public virtual IEnumerable<GenreViewModel> Genres { get; set; }
                = new List<GenreViewModel>();
    }
}
