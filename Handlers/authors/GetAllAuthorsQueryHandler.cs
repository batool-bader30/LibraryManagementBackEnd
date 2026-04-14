using LibraryManagement.DTO;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.Handlers
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<AuthorResponseDTO>>
    {
        private readonly IAuthorRepository _repo;

        public GetAllAuthorsQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AuthorResponseDTO>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _repo.GetAuthorsAsync();

            if (authors == null || authors.Count == 0)
            {
                throw new KeyNotFoundException("No authors found!");
            }

            // تحويل القائمة من Model إلى DTO
            return authors.Select(a => new AuthorResponseDTO
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                ImageUrl = a.ImageUrl, // <--- السطر السحري اللي ضفناه
                Books = a.books.Select(b => new BookSimpleDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    AuthorName = a.Name,
                    IsAvailable = b.IsAvailable
                }).ToList()
            }).ToList();
        }
    }
}