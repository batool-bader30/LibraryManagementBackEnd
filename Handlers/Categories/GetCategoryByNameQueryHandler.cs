using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.CategoryQueries;

namespace LibraryManagement.Handlers
{
    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, List<CategoryModel>>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoryByNameQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryModel>> Handle(GetCategoryByNameQuery request, CancellationToken ct)
        {
            var books = await _repo.GetCategoryByNameAsync(request.Name);

            if (books == null || books.Count == 0)
                throw new Exception("Book not found");

            return books;
        }
    }
}
