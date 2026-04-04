using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static LibraryManagement.command.BorrowingCommands;

namespace LibraryManagement.command
{
    public class BorrowingCommandHandlers :
        IRequestHandler<CreateBorrowingCommand, int>,
        IRequestHandler<UpdateBorrowingCommand, bool>,
        IRequestHandler<DeleteBorrowingByIdCommand, bool>
    {
        private readonly IBorrowingRepository _repo;
        private readonly IBookRepository _bookRepo;

        public BorrowingCommandHandlers(IBorrowingRepository repo, IBookRepository bookRepo)
        {
            _repo = repo;
            _bookRepo = bookRepo;
        }

        public async Task<int> Handle(CreateBorrowingCommand request, CancellationToken ct)
        {
            var book = await _bookRepo.GetBookByIdAsync(request.Borrowing.BookId);

            if (book == null)
                throw new ValidationException("Book not found.");

            // هذا هو السطر الذي يسبب الخطأ لأن الكتاب في الـ DB حالته false
            if (!book.IsAvailable)
                throw new ValidationException("Book is not available.");

            var newBorrow = new BorrowingModel
            {
                BookId = request.Borrowing.BookId,
                UserId = request.Borrowing.UserId,
                BorrowDate = DateTime.UtcNow,
                Status = BorrowStatus.Borrowing
            };

            await _repo.AddBorrowingAsync(newBorrow);
            return newBorrow.Id;
        }

        public async Task<bool> Handle(UpdateBorrowingCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetBorrowingByIdAsync(request.Id);
            if (existing == null) return false;

            existing.ReturnDate = request.Borrowing.ReturnDate;
            existing.Status = request.Borrowing.Status;

            await _repo.UpdateBorrowingAsync(existing);

            if (existing.Status == BorrowStatus.Returned)
            {
                var book = await _bookRepo.GetBookByIdAsync(existing.BookId);
                if (book != null)
                {
                    book.IsAvailable = true;
                    await _bookRepo.UpdateBookAsync(book);
                }
            }
            return true;
        }

        public async Task<bool> Handle(DeleteBorrowingByIdCommand request, CancellationToken ct)
        {
            return await _repo.DeleteBorrowingAsync(request.Id);
        }
    }
}