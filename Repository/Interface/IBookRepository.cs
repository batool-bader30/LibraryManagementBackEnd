using LibraryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interface
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task AddBookAsync(BookModel book);
        Task UpdateBookAsync(BookModel book);
        Task<bool> DeleteBookAsync(int id);
        Task<List<BookModel>> GetBooksByCategoryAsync(int categoryId);
        Task<List<BookModel>> GetBooksByAuthorAsync(int authorId);
        Task<List<BookModel>> GetAvailableBooksAsync();


    }
}
