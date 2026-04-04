using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using static LibraryManagement.query.BookQueries;
using LibraryManagement.DTO;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    public class GetBooksByAuthorHandler : IRequestHandler<GetBooksByAuthorQuery, List<BookSimpleDTO>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksByAuthorHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookSimpleDTO>> Handle(GetBooksByAuthorQuery request, CancellationToken ct)
        {
            var books = await _bookRepository.GetBooksByAuthorAsync(request.AuthorId);
            return books.Select(b => new BookSimpleDTO
            {
                Id = b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                AuthorName = b.Author?.Name,
                IsAvailable = b.IsAvailable
            }).ToList();
        }
    }
}
