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

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken ct)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(request.Id);
            if (existingBook == null) return false;

            existingBook.Description = request.Book.Description;
            existingBook.IsAvailable = request.Book.IsAvailable;

            await _bookRepository.UpdateBookAsync(existingBook);
            return true;
        }

       
    }
}
