using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using static LibraryManagement.query.BookQueries;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    public class GetBooksByCategoryHandler : IRequestHandler<GetBooksByCategoryQuery, List<BookModel>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksByCategoryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookModel>> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBooksByCategoryAsync(request.CategoryId);
        }
    }
}
