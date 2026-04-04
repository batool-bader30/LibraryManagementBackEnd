using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.CategoryQueries;

namespace LibraryManagement.Handlers
{


    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDTO>>
    {
        private readonly ICategoryRepository _repo;
        public GetAllCategoriesQueryHandler(ICategoryRepository repo) => _repo = repo;

        public async Task<List<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
        {
            var categories = await _repo.GetAllCategoriesAsync();

            // التحويل من Model لـ DTO يحل مشكلة الـ "object does not contain Name"
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}

