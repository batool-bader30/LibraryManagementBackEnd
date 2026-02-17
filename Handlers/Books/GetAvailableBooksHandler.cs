using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using static LibraryManagement.query.BookQueries;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    public class GetAvailableBooks : IRequestHandler<GetAvailableBooksQuery, List<BookModel>>
    {
        private readonly IBookRepository _repo;

        public GetAvailableBooks(IBookRepository repo)
        {
            _repo = repo;
        }



        public async Task<List<BookModel>> Handle(GetAvailableBooksQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAvailableBooksAsync();

        }
    }
}
