using BookVerse.Services.Core.Contracts;
using BookVerse.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookVerse.Web.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly ILogger<BookController> logger;
        public BookController(IBookService bookService, IGenreService genreService,
            ILogger<BookController> logger)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            IEnumerable<BookIndexViewModel> books 
                    = await bookService.GetAllBooksAsync(userId);

            return View(books);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            string? userId = GetUserId();

            BookDetailsViewModel? bookDetails  = await bookService
                        .GetBookDetailsByIdAsync(id, userId!);

            if (bookDetails == null)
            {
                TempData["ErrorMessage"] = "The request book does not exist!";
                return RedirectToAction(nameof(Index));
            }

            return View(bookDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BookFormModel createViewModel = await bookService.GetBookCreateViewModelAsync();

            return View(createViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookFormModel model)
        {
            model.Genres = await genreService
                .GetAllGenresOrderedByNameAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Isbn.Length != 10 && model.Isbn.Length != 13)
            {
                ModelState.AddModelError(nameof(model.Isbn), "ISBN must be either 10 or 13 characters long!");

                return View(model);
            }

            bool bookExists = await bookService
                .BoolExistsByIsbnAsync(model.Isbn);

            if (bookExists)
            {
                ModelState.AddModelError(nameof(model.Isbn), "A book with the same ISBN already exists!");

                return View(model);
            }

            bool genreExists = await genreService
                .ExistsByIdAsync(model.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(nameof(model.GenreId), "Selected genre does not exist!");
                
                return View(model);
            }
            try
            {
                string publisherId = GetUserId()!;//this is the userId

                await bookService.AddBookAsync(model,publisherId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "An error occurred while publishing a book");

                ModelState.AddModelError(string.Empty, "An error occurred while publishing the book. Please try again later.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyBooks()
        {

            IEnumerable<BookMyBooksViewModel> myBooks 
                = await bookService.GetMyBooksAsync(GetUserId()!);

            return View(myBooks);
        }


      
        [HttpPost]
        public async Task<IActionResult> AddToMyBooks(int id, string returnUrl)
        {
            string userId = GetUserId()!;

            returnUrl ??= Url.Action(nameof(Index))!;

            bool isUserPublisher = await bookService
                     .IsBookPublisherAsync(id, userId);

            if (isUserPublisher)
            {
                TempData["ErrorMessage"] = "You cannot add your own book to your collection!";

                return LocalRedirect(returnUrl);
            }

            bool isAlreadySaved = await bookService.IsBookSavedAsync(id, userId);

            if (!isAlreadySaved)
            {
                try
                {
                    await bookService.AddToMyBooksAsync(id, userId);
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "An error occurred while adding a book to favorites!");
                    TempData["ErrorMessage"] = "Unexpected error occurred while adding the book to your favorites! Please try again later.";          
                }
              
            }

           return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            string userId = GetUserId()!;

            bool isAlreadySaved = await bookService.IsBookSavedAsync(id, userId);

            if (isAlreadySaved)
            {
                try
                {
                    await bookService.RemoveFromMyBooksAsync(id, userId);
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "An error occurred while removing a book from favorites!");
                    TempData["ErrorMessage"] = "Unexpected error occurred while removing the book from your favorites! Please try again later.";
                }

            }

            return RedirectToAction(nameof(MyBooks));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookEditViewModel? bookEditViewModel 
                = await bookService.GetBookForEditAsync(id, GetUserId()!);


            if (bookEditViewModel == null)
            {
                TempData["ErrorMessage"] = "The request book does not exist or you are not the publisher!";
                return RedirectToAction(nameof(Index));
            }

            bookEditViewModel.Genres = await genreService.GetAllGenresOrderedByNameAsync();

            return View(bookEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel model)
        {
            model.Genres = await genreService
             .GetAllGenresOrderedByNameAsync();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Isbn.Length != 10 && model.Isbn.Length != 13)
            {
                ModelState.AddModelError(nameof(model.Isbn), "ISBN must be either 10 or 13 characters long!");

                return View(model);
            }

            bool bookExists = await bookService
                .BoolExistsByIsbnAsync(model.Isbn);

            if (bookExists)
            {
                ModelState.AddModelError(nameof(model.Isbn), "A book with the same ISBN already exists!");

                return View(model);
            }

            bool genreExists = await genreService
                .ExistsByIdAsync(model.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(nameof(model.GenreId), "Selected genre does not exist!");

                return View(model);
            }
            try
            {
                string publisherId = GetUserId()!;//this is the userId

                await bookService.EditBookAsync(model, publisherId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "An error occurred while publishing a book");

                ModelState.AddModelError(string.Empty, "An error occurred while publishing the book. Please try again later.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           

            BookDeleteViewModel? bookDeleteViewModel 
                = await bookService.GetBookDeleteDetailsAsync(id);

            if (bookDeleteViewModel == null)
            {
                TempData["ErrorMessage"] = "The request book does not exist!";
                return RedirectToAction(nameof(Index));
            }

            bool isUserPublisher = await bookService
                   .IsBookPublisherAsync(id, GetUserId()!);

            if (!isUserPublisher)
            {
                TempData["ErrorMessage"] = "You can delete only your own published books!";
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(bookDeleteViewModel);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            bool bookExists = await bookService.BookExistsByIdAsync(id);

            if (!bookExists)
            {
                TempData["ErrorMessage"] = "The request book does not exist!";
                return RedirectToAction(nameof(Index));
            }

            bool isUserPublisher = await bookService
                  .IsBookPublisherAsync(id, GetUserId()!);

            if (!isUserPublisher)
            {
                TempData["ErrorMessage"] = "You can delete only your own published books!";
                return RedirectToAction(nameof(Details), new { id });
            }
           
            try
            {
              await  bookService.DeleteBookAsync(id);

            }
            catch (Exception e)
            {
                this.logger.LogError(e, "An error occurred while deleting your book!");
                TempData["ErrorMessage"] = "Unexpected error occurred while deleting your book! Please try again later.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
