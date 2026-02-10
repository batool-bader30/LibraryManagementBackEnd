using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryManagement.models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.command.AuthorCommands;

namespace LibraryManagement.Handlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IAuthorRepository _repo;

        public CreateAuthorCommandHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAuthorByNameAsync(request.Author.Name);

            if (result != null && result.Count > 0)
                throw new Exception("Author already exists!");

            var author = new AuthorModel
            {
                Name = request.Author.Name,
                Bio = request.Author.Bio
            };

            await _repo.CreateAuthor(author);
            return author.Id;
        }
    }
}
