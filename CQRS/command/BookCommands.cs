using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.CQRS.command
{
    public static class BookCommands // خليتها static لترتيب أفضل
    {
        public record CreateBookCommand(CreateBookDTO Book) : IRequest<int>;
        
        public record DeleteBookByIdCommand(int Id) : IRequest<bool>;
        
        // التعديل: نستخدم UpdateBookDTO لضمان وصول كل البيانات المطلوبة للتعديل
        public record UpdateBookCommand(UpdateBookDTO Book) : IRequest<bool>;
    }
}