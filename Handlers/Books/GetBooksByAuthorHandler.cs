using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using static LibraryManagement.query.BookQueries;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    public class GetBooksByAuthorHandler : IRequestHandler<GetBooksByAuthorQuery, List<BookModel>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksByAuthorHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookModel>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBooksByAuthorAsync(request.AuthorId);
        }
    }
}
