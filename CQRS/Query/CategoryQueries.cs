using LibraryManagement.DTO;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.query
{
    public static class CategoryQueries
    {
        public record GetAllCategoriesQuery() : IRequest<List<CategoryDTO>>;
        public record GetCategoryByNameQuery(string Name) : IRequest<List<CategoryDTO>>;
    }
}
