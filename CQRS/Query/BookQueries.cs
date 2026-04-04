using LibraryManagement.DTO;
using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.query
{
    public static class BookQueries
    {
        // يرجع قائمة بسيطة (Id, Title, AuthorName, Image)
        public record GetAllBooksQuery() : IRequest<List<BookSimpleDTO>>;

        // يرجع التفاصيل الكاملة (Categories, Reviews, Bio) لصفحة التفاصيل
        public record GetBookByIdQuery(int Id) : IRequest<BookDetailedDTO?>;

        // يرجع قائمة بسيطة لكل كتب مؤلف معين
        public record GetBooksByAuthorQuery(int AuthorId) : IRequest<List<BookSimpleDTO>>;

        // يرجع قائمة بسيطة حسب التصنيف
        public record GetBooksByCategoryQuery(int CategoryId) : IRequest<List<BookSimpleDTO>>;

        // يرجع الكتب المتاحة فقط بصيغة بسيطة
        public record GetAvailableBooksQuery() : IRequest<List<BookSimpleDTO>>;
    }
}