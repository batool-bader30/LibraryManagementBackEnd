using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using static LibraryManagement.query.BookQueries;
using LibraryManagement.DTO;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookSimpleDTO>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

       public async Task<List<BookSimpleDTO>> Handle(GetAllBooksQuery request, CancellationToken ct)
{
    var books = await _bookRepository.GetAllBooksAsync(); // تأكدي أن الريبو يرجع List<BookModel>

    return books.Select(b => new BookSimpleDTO
    {
        Id = b.Id,
        Title = b.Title,
        ImageUrl = b.ImageUrl,
        AuthorName = b.Author?.Name ?? "Unknown",
        IsAvailable = b.IsAvailable
    }).ToList();
}
    }
}
