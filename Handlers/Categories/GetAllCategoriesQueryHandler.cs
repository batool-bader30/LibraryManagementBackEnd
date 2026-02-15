using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.CategoryQueries;

namespace LibraryManagement.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryModel>>
    {
        private readonly ICategoryRepository _repo;

        public GetAllCategoriesQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryModel>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
        {
            var books = await _repo.GetAllCategoriesAsync();
            if (books.Count == 0)
            {
                throw new Exception("No Books exists!");
            }
            return books;
        }
    }
}
