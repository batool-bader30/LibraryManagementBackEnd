using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.command;
using MediatR;
using static LibraryManagement.CQRS.command.BookCommands;

namespace LibraryManagement.CQRS.Handlers.Book.Commands
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBooks = await _bookRepository.GetAllBooksAsync();
            if (existingBooks.Any(b => b.ISBN == request.Book.ISBN))
                throw new Exception("Book with the same ISBN already exists.");

         BookModel newBook = new()
{
    Title = request.Book.Title,
    Description = request.Book.Description,
    ISBN = request.Book.ISBN,
    ImageUrl = request.Book.ImageUrl,
    AuthorId = request.Book.AuthorId,
    IsAvailable = true,
    BookCategories = new List<BookCategoryModel>() // <- هنا الحل
};

// ربط الكاتيجوريز
foreach (var catId in request.Book.CategoryIds)
{
    newBook.BookCategories.Add(new BookCategoryModel
    {
        CategoryId = catId
    });
}

await _bookRepository.AddBookAsync(newBook);
return newBook.Id;

        }
    }
}
