using LibraryManagement.data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDBcontext _context;

        public BookRepository(AppDBcontext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(BookModel book)
        {
            var authorExists = await _context.Authors
                .AnyAsync(a => a.Id == book.AuthorId);

            if (!authorExists)
                throw new Exception("Author does not exist");

            var bookExists = await _context.Books
                .AnyAsync(b => b.ISBN == book.ISBN);

            if (bookExists)
                throw new Exception("Book with same ISBN already exists");

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            return await _context.Books
    .Include(b => b.Author)
        .ThenInclude(a => a.books) 
    .Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category)
    .Include(b => b.Borrowings)
    .Include(b => b.Reviews).ToListAsync();

        }

        public async Task<BookModel?> GetBookByIdAsync(int id)
        {
            return await _context.Books
    .Include(b => b.Author)
        .ThenInclude(a => a.books) 
    .Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category)
    .Include(b => b.Borrowings)
    .Include(b => b.Reviews)
    .FirstOrDefaultAsync(b => b.Id == id);

        }

        public async Task<List<BookModel>> GetAvailableBooksAsync()
        {
             return await _context.Books
             .Where(b => b.IsAvailable)
    .Include(b => b.Author)
        .ThenInclude(a => a.books) 
    .Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category)
    .Include(b => b.Borrowings)
    .Include(b => b.Reviews).ToListAsync();
           
        }

        public async Task<List<BookModel>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
        .ThenInclude(a => a.books) 
    .Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category)
    .Include(b => b.Borrowings)
    .Include(b => b.Reviews).ToListAsync();
        }

        public async Task<List<BookModel>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Books
                .Include(b => b.BookCategories)
                .Where(b => b.BookCategories
                    .Any(bc => bc.CategoryId == categoryId))
                .Include(b => b.Author)
        .ThenInclude(a => a.books) 
    .Include(b => b.Borrowings)
    .Include(b => b.Reviews).ToListAsync();
        }

        public async Task UpdateBookAsync(BookModel book)
        {
            var existingBook = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            if (existingBook == null)
                throw new Exception("Book not found");

            existingBook.Title = book.Title;
            existingBook.Description = book.Description;
            existingBook.ISBN = book.ISBN;
            existingBook.ImageUrl = book.ImageUrl;
            existingBook.AuthorId = book.AuthorId;
            existingBook.IsAvailable = book.IsAvailable;

            await _context.SaveChangesAsync();
        }
    }
}
