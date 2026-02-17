using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static LibraryManagement.command.BorrowingCommands;

namespace LibraryManagement.command
{
    public class BorrowingCommandHandlers
    {
        public class CreateBorrowingHandler : IRequestHandler<CreateBorrowingCommand, int>
        {
            private readonly IBorrowingRepository _repo;
            private readonly IBookRepository _bookRepo;

            public CreateBorrowingHandler(IBorrowingRepository repo, IBookRepository bookRepo)
            {
                _repo = repo;
                _bookRepo = bookRepo;
            }

            public async Task<int> Handle(CreateBorrowingCommand request, CancellationToken cancellationToken)
            {
                // جلب الكتاب
                var book = await _bookRepo.GetBookByIdAsync(request.Borrowing.BookId);
                if (book == null)
                    throw new ValidationException("Book not found.");

                // تحقق من توفر الكتاب
                if (!book.IsAvailable)
                    throw new ValidationException("Book is not available for borrowing.");

                // إنشاء Borrowing جديد
                var newBorrow = new BorrowingModel
                {
                    BookId = request.Borrowing.BookId,
                    UserId = request.Borrowing.UserId.ToString(),
                    BorrowDate = request.Borrowing.BorrowDate,
                    ReturnDate = request.Borrowing.ReturnDate,
                    Status = request.Borrowing.Status
                };

                await _repo.AddBorrowingAsync(newBorrow);

                // بعد الاستعارة، الكتاب يصير غير متاح
                book.IsAvailable = false;
                await _bookRepo.UpdateBookAsync(book);

                return newBorrow.Id;
            }
        }

        public class UpdateBorrowingHandler : IRequestHandler<UpdateBorrowingCommand, bool>
        {
            private readonly IBorrowingRepository _repo;
            private readonly IBookRepository _bookRepo;

            public UpdateBorrowingHandler(IBorrowingRepository repo, IBookRepository bookRepo)
            {
                _repo = repo;
                _bookRepo = bookRepo;
            }

            public async Task<bool> Handle(UpdateBorrowingCommand request, CancellationToken cancellationToken)
            {
                var existingBorrow = await _repo.GetBorrowingByIdAsync(request.Id);
                if (existingBorrow == null)
                    throw new ValidationException("Borrowing record not found.");

                // تحديث البيانات
                existingBorrow.BorrowDate = request.Borrowing.BorrowDate;
                existingBorrow.ReturnDate = request.Borrowing.ReturnDate;
                existingBorrow.Status = request.Borrowing.Status;

                await _repo.UpdateBorrowingAsync(existingBorrow);

                // تحديث حالة الكتاب حسب BorrowStatus
                var book = await _bookRepo.GetBookByIdAsync(existingBorrow.BookId);
                if (book != null)
                {
                    book.IsAvailable = existingBorrow.Status == BorrowStatus.Returned;
                    await _bookRepo.UpdateBookAsync(book);
                }

                return true;
            }
        }

        public class DeleteBorrowingHandler : IRequestHandler<DeleteBorrowingByIdCommand, bool>
        {
            private readonly IBorrowingRepository _repo;
            private readonly IBookRepository _bookRepo;

            public DeleteBorrowingHandler(IBorrowingRepository repo, IBookRepository bookRepo)
            {
                _repo = repo;
                _bookRepo = bookRepo;
            }

            public async Task<bool> Handle(DeleteBorrowingByIdCommand request, CancellationToken cancellationToken)
            {
                var borrow = await _repo.GetBorrowingByIdAsync(request.Id);
                if (borrow == null) return false;

                // عند الحذف، لو الكتاب كان مستعار، نرجع حالته متاح
                if (borrow.Status == BorrowStatus.Borrowing)
                {
                    var book = await _bookRepo.GetBookByIdAsync(borrow.BookId);
                    if (book != null)
                    {
                        book.IsAvailable = true;
                        await _bookRepo.UpdateBookAsync(book);
                    }
                }

                return await _repo.DeleteBorrowingAsync(request.Id);
            }
        }
    }
}
