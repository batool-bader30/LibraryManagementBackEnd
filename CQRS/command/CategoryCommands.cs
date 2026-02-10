using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.command
{
    public static class CategoryCommands
    {
        public record CreateCategoryCommand(CategoryDto Category) : IRequest<int>;
        public record DeleteCategoryByIdCommand(int Id) : IRequest<bool>;
    }
}
