using LibraryManagement.DTO;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.command.AuthorCommands;

namespace LibraryManagement.Handlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>
    {
        private readonly IAuthorRepository _repo;

        public UpdateAuthorCommandHandler(IAuthorRepository repo) => _repo = repo;

        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _repo.GetAuthorByIdAsync(request.Id);
            if (author == null) return false;

            author.Name = request.Author.Name;
            author.Bio = request.Author.Bio;

            await _repo.UpdateAuthorAsync(author);
            return true;
        }
    }
}