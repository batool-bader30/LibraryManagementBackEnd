using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Hosting;

using static LibraryManagement.CQRS.command.BookCommands;

namespace LibraryManagement.Handlers.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookRepository _repo;

        public CreateBookCommandHandler(IBookRepository repo)
        {
            _repo = repo;
        }
        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetBooksByTitleAsync(request.Book.Title);

            if (result != null && result.Count > 0)
                throw new Exception("Book already exists!");


            var book = new BookModel
            {
                Title = request.Book.Title,
                Description = request.Book.Description,
                Price = request.Book.Price,
                PublishYear = request.Book.PublishYear,
                ISBN = request.Book.ISBN,
                AuthorId = request.Book.AuthorId
            };

            if (request.Book.ImageUrl != null && request.Book.ImageUrl.Length > 0)
            {
                using var stream = new MemoryStream();
                await request.Book.ImageUrl.CopyToAsync(stream, cancellationToken);
                book.ImageUrl = stream.ToArray(); // خزني الصورة byte[] مباشرة
            }

            await _repo.CreateBook(book);

            return book.Id;

        }
    }
}