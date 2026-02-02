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
    public record GetallAuthorsQuery() :IRequest<List<Authormodel>>;
        public record GetAuthorsByNameQuery(string Name) : IRequest<List<Authormodel>>;
        public record GetAllAuthorsQuery() : IRequest<List<Authormodel>>;
        
    }

    
}