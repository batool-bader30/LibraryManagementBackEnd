using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.query
{
   public static class BookQueries
    {
        public record GetAllBooksQuery() : IRequest<List<BookModel>>;
        public record GetBookByIdQuery(int Id) : IRequest<BookModel?>;
        public record GetBooksByAuthorQuery(int AuthorId) : IRequest<List<BookModel>>;
        public record GetBooksByCategoryQuery(int CategoryId) : IRequest<List<BookModel>>;
        public record GetAvailableBooksQuery() : IRequest<List<BookModel>>;
    }
}