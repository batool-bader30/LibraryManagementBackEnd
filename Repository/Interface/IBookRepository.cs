using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interface
{
    public interface IBookRepository
    {
        // نغيرها لترجع Model، والتحويل لـ DTO بصير بالـ Handler
        Task<List<BookModel>> GetAllBooksAsync(); 
        Task<BookModel?> GetBookByIdAsync(int id);
        Task<List<BookModel>> GetBooksByCategoryAsync(int categoryId);
        Task<List<BookModel>> GetBooksByAuthorAsync(int authorId);
        Task<List<BookModel>> GetAvailableBooksAsync();
        Task<List<BookModel>> GetBooksByCategoryIdAsync(int categoryId);
        Task AddBookAsync(BookModel book);
        Task UpdateBookAsync(BookModel book);
        Task<bool> DeleteBookAsync(int id);
    }
}