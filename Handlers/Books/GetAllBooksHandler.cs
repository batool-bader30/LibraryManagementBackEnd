using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using static LibraryManagement.query.BookQueries;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookModel>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAllBooksAsync();
        }
    }
}
