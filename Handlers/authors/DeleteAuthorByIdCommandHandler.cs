using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.command.AuthorCommands;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.Handlers
{
    public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, bool>
    {
        private readonly IAuthorRepository _repo;

        public DeleteAuthorByIdCommandHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }


        public async Task<bool> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeletAuthorbyid(request.Id);


        }
    }
}
