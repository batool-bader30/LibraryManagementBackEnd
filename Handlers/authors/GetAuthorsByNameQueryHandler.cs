using LibraryManagement.DTO;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.Handlers
{
    public class GetAuthorsByNameQueryHandler : IRequestHandler<GetAuthorsByNameQuery, List<AuthorResponseDTO>>
    {
        private readonly IAuthorRepository _repo;

        public GetAuthorsByNameQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AuthorResponseDTO>> Handle(GetAuthorsByNameQuery request, CancellationToken ct)
        {
            // جلب البيانات من المستودع (Repository)
            var authors = await _repo.GetAuthorByNameAsync(request.Name);

            // التحقق إذا كانت القائمة فارغة
            if (authors == null || authors.Count == 0)
            {
                // رمي استثناء يوضح أن المؤلف غير موجود
                throw new KeyNotFoundException($"Author with name '{request.Name}' does not exist!");
            }

            // تحويل النتائج إلى DTO لقطع العلاقات الدائرية
            return authors.Select(a => new AuthorResponseDTO
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                 ImageUrl = a.ImageUrl,
                Books = a.books.Select(b => new BookSimpleDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    AuthorName = a.Name?? "",
                    IsAvailable = b.IsAvailable
                }).ToList()
            }).ToList();
        }
    }
}