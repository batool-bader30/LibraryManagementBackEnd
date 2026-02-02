using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryManagement.models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.Handlers
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Authormodel>>
    {
        private readonly IAuthorRepository _repo;

        public GetAllAuthorsQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Authormodel>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAuthorsAsync();
        }
    }
}
