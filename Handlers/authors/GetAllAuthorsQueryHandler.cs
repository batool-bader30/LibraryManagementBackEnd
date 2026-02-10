using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryManagement.models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.Handlers
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<AuthorModel>>
    {
        private readonly IAuthorRepository _repo;

        public GetAllAuthorsQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AuthorModel>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors =await _repo.GetAuthorsAsync();
            if (authors.Count == 0)
            {
                throw new Exception("no authors exists!");
            }
            return authors;
        }
    }
}
