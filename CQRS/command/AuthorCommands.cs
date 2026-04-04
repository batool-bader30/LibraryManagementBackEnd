using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.command // تأكدي إن هاد الـ namespace موحد في كل الـ Commands
{
    public static class AuthorCommands
    {
        // 1. إضافة مؤلف جديد - يرجع الـ ResponseDTO اللي جواه BookSimpleDTO
        public record CreateAuthorCommand(CreateAuthorDTO Author) : IRequest<AuthorResponseDTO>;

        // 2. حذف مؤلف - يرجع true/false
        public record DeleteAuthorByIdCommand(int Id) : IRequest<bool>;

        // 3. تعديل مؤلف - يستخدم UpdateAuthorDTO (Name, Bio)
        public record UpdateAuthorCommand(int Id, UpdateAuthorDTO Author) : IRequest<bool>;
    }
}