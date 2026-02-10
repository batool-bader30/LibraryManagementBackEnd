using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.models;
using MediatR;

namespace LibraryManagement.query
{
    public static class AuthorQuerys
    {
        public record GetAuthorsByNameQuery(string Name) : IRequest<List<AuthorModel>>;
        public record GetAllAuthorsQuery() : IRequest<List<AuthorModel>>;

    }


}