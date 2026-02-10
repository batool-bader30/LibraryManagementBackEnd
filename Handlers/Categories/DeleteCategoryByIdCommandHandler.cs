using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.command.CategoryCommands;

namespace LibraryManagement.Handlers
{
    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, bool>
    {
        private readonly ICategoryRepository _repo;

        public DeleteCategoryByIdCommandHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteCategoryByIdCommand request, CancellationToken ct)
        {
            return await _repo.DeleteCategoryById(request.Id);
        }
    }
}
