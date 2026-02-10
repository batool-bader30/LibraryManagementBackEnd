using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.Handlers
{
    public class GetAuthorsByNameQueryHandler : IRequestHandler<GetAuthorsByNameQuery, List<AuthorModel>>
    {
        private readonly IAuthorRepository _repo;

        public GetAuthorsByNameQueryHandler(IAuthorRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<AuthorModel>> Handle(GetAuthorsByNameQuery request, CancellationToken ct)
        {
             var authors =await _repo.GetAuthorByNameAsync(request.Name);
            if (authors.Count == 0)
            {
                throw new Exception("author not exists!");
            }
            return authors;
        }
    }
}