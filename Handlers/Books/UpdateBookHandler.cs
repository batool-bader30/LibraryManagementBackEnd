using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.command;
using MediatR;
using static LibraryManagement.CQRS.command.BookCommands;

namespace LibraryManagement.CQRS.Handlers.Book.Commands
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(request.Book.Id);
            if (existingBook == null)
                return false;

            existingBook.Title = request.Book.Title;
            existingBook.Description = request.Book.Description;
            existingBook.ISBN = request.Book.ISBN;
            existingBook.ImageUrl = request.Book.ImageUrl;
            existingBook.AuthorId = request.Book.AuthorId;

            await _bookRepository.UpdateBookAsync(existingBook);
            return true;
        }
    }
}
