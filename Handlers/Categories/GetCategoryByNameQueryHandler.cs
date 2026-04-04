using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.CategoryQueries;

namespace LibraryManagement.Handlers
{
    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, List<CategoryDTO>>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoryByNameQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryDTO>> Handle(GetCategoryByNameQuery request, CancellationToken ct)
        {
            var categories = await _repo.GetCategoryByNameAsync(request.Name);
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}
