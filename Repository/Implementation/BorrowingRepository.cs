using LibraryManagement.data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository.Implementation
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly AppDBcontext _context;

        public BorrowingRepository(AppDBcontext context)
        {
            _context = context;
        }

        public async Task AddBorrowingAsync(BorrowingModel borrowing)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == borrowing.BookId);

            if (book == null)
                throw new Exception("Book not found");

            if (!book.IsAvailable)
                throw new Exception("Book is not available");

            var userExists = await _context.Users
                .AnyAsync(u => u.Id == borrowing.UserId);

            if (!userExists)
                throw new Exception("User not found");

            book.IsAvailable = false;

            await _context.Borrowings.AddAsync(borrowing);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBorrowingAsync(int id)
        {
            var borrow = await _context.Borrowings.FindAsync(id);
            if (borrow == null)
                return false;

            _context.Borrowings.Remove(borrow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BorrowingModel>> GetAllBorrowingsAsync()
        {
            return await _context.Borrowings
          .Include(b => b.User)
          .Include(b => b.Book)
              .ThenInclude(book => book.Author) // هذا السطر سيجلب بيانات المؤلف المرتبط بالكتاب
          .ToListAsync();
        }

        public async Task<BorrowingModel?> GetBorrowingByIdAsync(int id)
        {
            return await _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<BorrowingModel>> GetActiveBorrowingsAsync()
        {
            return await _context.Borrowings
                .Where(b => b.ReturnDate == null)
                .ToListAsync();
        }


        public async Task<List<BorrowingModel>> GetBorrowingsByUserIdAsync(string userId)
        {
            return await _context.Borrowings
                .Include(b => b.User)          // جلب بيانات المستخدم
                .Include(b => b.Book)          // جلب بيانات الكتاب
                    .ThenInclude(book => book.Author) // !!! السطر الناقص: جلب بيانات المؤلف التابع للكتاب
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
        public async Task UpdateBorrowingAsync(BorrowingModel borrowing)
        {
            var existing = await _context.Borrowings
                .FirstOrDefaultAsync(b => b.Id == borrowing.Id);

            if (existing == null)
                throw new Exception("Borrowing not found");

            existing.ReturnDate = borrowing.ReturnDate;
            existing.BorrowDate = borrowing.BorrowDate;

            await _context.SaveChangesAsync();
        }
    }
}
