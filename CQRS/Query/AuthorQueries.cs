using LibraryManagement.DTO;
using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.query // تأكدي إن هاد الـ namespace موحد في كل الـ Queries
{
    public static class AuthorQuerys
    {
        // 1. جلب كل المؤلفين - يرجع لستة من الـ ResponseDTO
        public record GetAllAuthorsQuery() : IRequest<List<AuthorResponseDTO>>;

        // 2. البحث عن مؤلفين بالاسم
        public record GetAuthorsByNameQuery(string Name) : IRequest<List<AuthorResponseDTO>>;

        // 3. جلب مؤلف واحد بالـ ID (مهم جداً لصفحة البروفايل في Flutter)
        public record GetAuthorByIdQuery(int Id) : IRequest<AuthorResponseDTO>;
    }
}