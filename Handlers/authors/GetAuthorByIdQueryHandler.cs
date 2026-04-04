using LibraryManagement.DTO;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.Handlers
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorResponseDTO>
    {
        private readonly IAuthorRepository _repo;

        public GetAuthorByIdQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<AuthorResponseDTO> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. جلب المؤلف من الـ Repository باستخدام الـ Id
            var author = await _repo.GetAuthorByIdAsync(request.Id);

            // 2. إذا مش موجود، بنرجع null أو بنرمي Exception حسب رغبتك
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} not found.");
            }

            // 3. تحويل (Mapping) من Model إلى DTO
            return new AuthorResponseDTO
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                Books = author.books.Select(b => new BookSimpleDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    AuthorName = author.Name,
                    IsAvailable = b.IsAvailable
                }).ToList()
            };
        }
    }
}