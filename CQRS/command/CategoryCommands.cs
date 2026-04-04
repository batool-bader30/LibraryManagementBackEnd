using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.command
{
    public static class CategoryCommands
    {
        public record CreateCategoryCommand(CreateCategoryDTO Category) : IRequest<int>;
        public record UpdateCategoryCommand(int Id, UpdateCategoryDTO Category) : IRequest<bool>;
        public record DeleteCategoryByIdCommand(int Id) : IRequest<bool>;
    }
}
