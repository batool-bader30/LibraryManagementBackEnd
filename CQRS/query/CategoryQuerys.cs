using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.query
{
    public static class CategoryQueries
    {
        public record GetAllCategoriesQuery() : IRequest<List<CategoryModel>>;
        public record GetCategoryByNameQuery(string Name) : IRequest<List<CategoryModel>>;
    }
}
