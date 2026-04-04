using LibraryManagement.Repository.Interface;
using LibraryManagement.command;
using MediatR;
using static LibraryManagement.CQRS.command.BookCommands;

namespace LibraryManagement.CQRS.Handlers.Book.Commands
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookByIdCommand, bool>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(DeleteBookByIdCommand request, CancellationToken ct)
        {
            return await _bookRepository.DeleteBookAsync(request.Id);
        }
    }
}
