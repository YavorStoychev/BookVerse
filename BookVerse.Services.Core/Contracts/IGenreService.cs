using BookVerse.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Services.Core.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetAllGenresUnOrderedAsync();

        Task<IEnumerable<GenreViewModel>> GetAllGenresOrderedByNameAsync();

        Task<bool> ExistsByIdAsync(int id);

    }
}
