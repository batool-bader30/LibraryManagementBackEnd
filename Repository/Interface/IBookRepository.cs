using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.models;

namespace LibraryManagement.Repository.Interface
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetBooksAsync();
        Task<List<BookModel>> GetBooksByTitleAsync(string title);
        Task<List<BookModel>> GetBooksByAuthorNameAsync(string authorName);
        Task CreateBook(BookModel book);
        Task<bool> DeleteBookById(int id);
        Task<bool> DeleteBookByAuthorId(int id);


    }

}