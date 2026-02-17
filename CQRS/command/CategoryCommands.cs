using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.command
{
    public static class CategoryCommands
    {
        public record CreateCategoryCommand(CategoryDTO Category) : IRequest<int>;
        public record DeleteCategoryByIdCommand(int Id) : IRequest<bool>;
    }
}
