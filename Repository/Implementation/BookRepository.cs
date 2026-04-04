using LibraryManagement.data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly AppDBcontext _context;

    public BookRepository(AppDBcontext context)
    {
        _context = context;
    }

    // 1. جلب الكل مع العلاقات الأساسية
    public async Task<List<BookModel>> GetAllBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .ToListAsync();
    }

    // 2. جلب كتاب واحد مع كل التفاصيل (ضروري لـ BookDetailedDTO)
    public async Task<BookModel?> GetBookByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .Include(b => b.Reviews)
                .ThenInclude(r => r.User) // عشان نجيب اسم اللي عمل التقييم
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    // 3. جلب حسب التصنيف (بدون المابينج اليدوي)
    public async Task<List<BookModel>> GetBooksByCategoryAsync(int categoryId)
    {
        return await _context.Books
            .Where(b => b.BookCategories.Any(bc => bc.CategoryId == categoryId))
            .Include(b => b.Author)
            .ToListAsync();
    }

    // بقية الميثودز (Update, Delete, Add) اللي عندك صحيحة وممتازة
    public async Task AddBookAsync(BookModel book)
    {
        // التحقق من المؤلف والـ ISBN موجود في كودك وهو صحيح 100%
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(BookModel book)
    {
        var existingBook = await _context.Books.FindAsync(book.Id);
        if (existingBook == null) throw new Exception("Book not found");

        _context.Entry(existingBook).CurrentValues.SetValues(book); // طريقة أسرع للتحديث
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<BookModel>> GetBooksByAuthorAsync(int authorId)
    {
        return await _context.Books
            .Where(b => b.AuthorId == authorId)
            .Include(b => b.Author)
            .ToListAsync();
    }

    public async Task<List<BookModel>> GetAvailableBooksAsync()
    {
        return await _context.Books
            .Where(b => b.IsAvailable)
            .Include(b => b.Author)
            .ToListAsync();
    }
    public async Task<List<BookModel>> GetBooksByCategoryIdAsync(int categoryId)
    {
        return await _context.Books
            .Include(b => b.Author) // مهم عشان الـ AuthorName في الـ DTO ما يطلع null
            .Where(b => b.BookCategories.Any(bc => bc.CategoryId == categoryId))
            .ToListAsync();
    }
}