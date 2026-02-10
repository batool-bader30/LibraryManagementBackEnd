using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.models;
using MediatR;

namespace LibraryManagement.query
{
    public class BookQuerys
    {
        
        public record GetBooksByAuthorNameAsync(string AuthorName) : IRequest<List<BookModel>>;
        public record GetBooksByTitleQuery(string Title) : IRequest<List<BookModel>>;
        public record GetAllBooksQuery() : IRequest<List<BookModel>>;

    }
}