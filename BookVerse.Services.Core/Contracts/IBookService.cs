using BookVerse.ViewModels.Book;
using BookVerse.ViewModels.Genre;

namespace BookVerse.Services.Core.Contracts
{
    public interface IBookService
    {
        Task AddBookAsync(BookFormModel model, string publisherId);

        Task AddToMyBooksAsync(int bookId, string userId);

        Task DeleteBookAsync(int bookId);

        Task<IEnumerable<BookIndexViewModel>>GetAllBooksAsync(string? userId);


      

        Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int bookId, string? userId);

        Task<bool> IsBookSavedAsync(int bookId, string userId);

        Task<bool> IsBookPublisherAsync(int bookId, string userId);

        Task<BookFormModel> GetBookCreateViewModelAsync();


        Task<IEnumerable<BookMyBooksViewModel>>GetMyBooksAsync(string userId);

        Task RemoveFromMyBooksAsync(int bookId, string userId);

        Task<BookEditViewModel?> GetBookForEditAsync(int id, string userId);

        Task EditBookAsync(BookEditViewModel model, string userId);
        Task<BookDeleteViewModel> GetBookDeleteDetailsAsync(int id);

        Task<bool> BoolExistsByIsbnAsync(string isbn);

        Task<bool> BookExistsByIdAsync(int id);
    }
}
