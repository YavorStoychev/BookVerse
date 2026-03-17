using BookVerse.Data;
using BookVerse.DataModels;
using BookVerse.Services.Core.Contracts;
using BookVerse.ViewModels.Book;
using BookVerse.ViewModels.Genre;
using Microsoft.EntityFrameworkCore;
using static BookVerse.GCommon.ApplicationConstants;
namespace BookVerse.Services.Core
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext dbContext;
        public BookService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddBookAsync(BookFormModel model, string publisherId)
        {
           Book book = new Book()
            {
                Isbn = model.Isbn,
                Title = model.Title,
                Description = model.Description,
                CoverImageUrl = model.CoverImageUrl,
                PublishedOn = DateOnly.FromDateTime(model.PublishedOn),
                GenreId = model.GenreId,
                PublisherId = publisherId
            };

           await dbContext.Books.AddAsync(book);
           await dbContext.SaveChangesAsync();
        }

        public async Task AddToMyBooksAsync(int bookId, string userId)
        {
            Book? book = await dbContext
                 .Books
                 .FindAsync(bookId);

            if (book != null)
            {
                UserBook userBook = new UserBook()
                {
                    BookId = bookId,
                    UserId = userId
                };

               await dbContext.UsersBooks.AddAsync(userBook);
               await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            Book? bookToDelete = await dbContext
                .Books
                .FindAsync(bookId);

            if (bookToDelete != null)
            {
                bookToDelete.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task EditBookAsync(BookEditViewModel model, string userId)
        {

            Book? editBook = await dbContext
                .Books
                .SingleOrDefaultAsync(b => b.Id == model.Id && 
                b.PublisherId.ToLower() == userId.ToLower());
          

            if (editBook == null) 
            {
                throw new ArgumentException("Event with the provided ID does not exist in this current db");
            }

            editBook.Isbn = model.Isbn;
            editBook.Title = model.Title;
            editBook.Description = model.Description;
            editBook.CoverImageUrl = model.CoverImageUrl;
            editBook.PublishedOn = model.PublishedOn;
            editBook.GenreId = model.GenreId;

            dbContext.Books.Update(editBook);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookIndexViewModel>> GetAllBooksAsync(string? userId)
        {
            IEnumerable<BookIndexViewModel> allBooks = await dbContext
                .Books
                .Include(b => b.Genre)
                .Include(b => b.UsersBooks)
                .AsNoTracking()
                .Select(b => new BookIndexViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverImageUrl = b.CoverImageUrl,
                    Genre = b.Genre.Name,
                    IsAuthor = userId != null && b.PublisherId.ToLower() == userId.ToLower(),
                    IsSaved = userId != null && b.UsersBooks.Any(ub => ub.UserId.ToLower() == userId.ToLower()),
                    SavedCount = b.UsersBooks.Count()
                })
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Genre)
                .ThenByDescending(b => b.SavedCount)
                .ToArrayAsync();

            return allBooks;
        }
        

       
        public async Task<BookFormModel> GetBookCreateViewModelAsync()
        {
           BookFormModel model = new BookFormModel()
            {
                Genres = await dbContext
                    .Genres
                    .AsNoTracking()
                    .Select(g => new GenreViewModel()
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .OrderBy(g => g.Name)
                    .ToArrayAsync()
            };

            return model;
        }

        public async Task<BookDeleteViewModel> GetBookDeleteDetailsAsync(int id)
        {
            BookDeleteViewModel? bookDeleteViewModel = await dbContext
                .Books
                .Include(b => b.Publisher)
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BookDeleteViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Publisher = b.Publisher.UserName!
                })
                .SingleOrDefaultAsync();

            return bookDeleteViewModel;
        }

        public async Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int bookId, string? userId)
        {
            BookDetailsViewModel? bookDetails = await dbContext
                .Books
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.UsersBooks)
                .AsNoTracking()
                .Where(b => b.Id == bookId)
                .Select(b => new BookDetailsViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    CoverImageUrl = b.CoverImageUrl,
                    PublishedOn = b.PublishedOn.ToString(DateFormat),
                    Publisher = b.Publisher.UserName!,
                    IsAuthor = userId != null && b.PublisherId.ToLower() == userId.ToLower(),
                    IsSaved = userId != null && b.UsersBooks.Any(ub => ub.UserId.ToLower() == userId.ToLower())
                })
                .SingleOrDefaultAsync();

            return bookDetails;
        }

        public async Task<BookEditViewModel?> GetBookForEditAsync(int id, string userId)
        {
           Book? book = await dbContext.Books
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.UsersBooks)
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id && b.PublisherId.ToLower() == userId.ToLower());

            if (book == null)
            {
                return null;
            }

            BookEditViewModel model = new BookEditViewModel()
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                Description = book.Description,
                CoverImageUrl = book.CoverImageUrl,
                PublishedOn = book.PublishedOn,
                GenreId = book.GenreId,              
            };

            return model;
        }

        public async Task<IEnumerable<BookMyBooksViewModel>> GetMyBooksAsync(string userId)
        {
            IEnumerable<BookMyBooksViewModel> allUserBooks = await dbContext
                .Books
                .Include(b => b.Genre)
                .Include(b => b.UsersBooks)
                .AsNoTracking()
                .Where(b => b.UsersBooks.Any(ub => ub.UserId.ToLower() == userId.ToLower()))
                .Select(b => new BookMyBooksViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverImageUrl = b.CoverImageUrl,
                    Genre = b.Genre.Name
                })
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Genre)
                .ToArrayAsync();

            return allUserBooks;
        }

        public async Task<bool> IsBookPublisherAsync(int bookId, string userId)
        {
            bool isPublisher = false;

            if (!string.IsNullOrEmpty(userId))
            {
                isPublisher = await dbContext
                    .Books
                    .AnyAsync(b => b.Id == bookId && 
                        b.PublisherId.ToLower() == userId.ToLower());
            }

            return isPublisher;
        }

        public async Task<bool> IsBookSavedAsync(int bookId, string userId)
        {
            bool isAlreadySaved = false;

            if (!string.IsNullOrEmpty(userId))
            {
                isAlreadySaved = await dbContext
                    .UsersBooks
                    .AnyAsync(ub => ub.UserId.ToLower() == userId.ToLower() && 
                                    ub.BookId == bookId);
            }

            return isAlreadySaved;
        }

        public async Task RemoveFromMyBooksAsync(int bookId, string userId)
        {
            UserBook? userBookToDelete = await dbContext
                .UsersBooks
                .AsNoTracking()
                .SingleOrDefaultAsync(ub => ub.UserId.ToLower() == userId &&
                                    ub.BookId == bookId);

            if (userBookToDelete != null)
            {
                dbContext.UsersBooks.Remove(userBookToDelete);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> BoolExistsByIsbnAsync(string isbn)
        {
           return await dbContext 
                .Books
                .AsNoTracking()
                .AnyAsync(b => b.Isbn.ToLower() == isbn.ToLower());
          
        }

        public async Task<bool> BookExistsByIdAsync(int id)
        {
            bool bookExists = await dbContext
                .Books
                .AnyAsync(b => b.Id == id);

            return bookExists;
        }
    }
}
