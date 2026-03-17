using BookVerse.Data;
using BookVerse.DataModels;
using BookVerse.Services.Core.Contracts;
using BookVerse.ViewModels.Genre;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Services.Core
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext dbContext;
        public GenreService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<GenreViewModel>> GetAllGenresUnOrderedAsync()
        {
            IEnumerable<GenreViewModel> allGenres = await dbContext
                .Genres
                .AsNoTracking()
                .Select(g => new GenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToArrayAsync();

            return allGenres;
        }
        public async Task<IEnumerable<GenreViewModel>> GetAllGenresOrderedByNameAsync()
        {
            IEnumerable<GenreViewModel> allGenres = await dbContext
                 .Genres
                 .AsNoTracking()
                 .Select(g => new GenreViewModel()
                 {
                     Id = g.Id,
                     Name = g.Name
                 })
                 .OrderBy(g => g.Name)
                 .ToArrayAsync();

            return allGenres;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
           return await dbContext.Genres                
                .AnyAsync(g => g.Id == id);
        }
    }
}
