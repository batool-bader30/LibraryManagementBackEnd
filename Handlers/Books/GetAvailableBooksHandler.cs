using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.query;
using MediatR;
using LibraryManagement.DTO; // تأكدي من إضافة هذا الـ namespace
using static LibraryManagement.query.BookQueries;

namespace LibraryManagement.CQRS.Handlers.Book.Queries
{
    // الخطوة 1: تغيير نوع الإرجاع في الـ Interface ليكون BookSimpleDTO
    public class GetAvailableBooks : IRequestHandler<GetAvailableBooksQuery, List<BookSimpleDTO>>
    {
        private readonly IBookRepository _repo;

        public GetAvailableBooks(IBookRepository repo)
        {
            _repo = repo;
        }

        // الخطوة 2: تعديل ميثود الـ Handle لتقوم بعمل Mapping (تحويل)
        public async Task<List<BookSimpleDTO>> Handle(GetAvailableBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repo.GetAvailableBooksAsync();

            // تحويل من Model إلى DTO لضمان توافق الأنواع ومنع الـ Circular Dependency
            return books.Select(b => new BookSimpleDTO
            {
                Id = b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                IsAvailable = b.IsAvailable,
                AuthorName = b.Author?.Name ?? "Unknown" // تأكدي أن الـ Repository يعمل Include للـ Author
            }).ToList();
        }
    }
}