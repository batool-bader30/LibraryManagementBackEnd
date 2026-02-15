using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateBook(BookModel book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }



        public async Task<bool> DeleteBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<BookModel>> GetBooksAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();

        }

        public async Task<List<BookModel>> GetBooksByAuthorNameAsync(string authorName)
        {
            return await _context.Books.Include(b => b.Author)
                                   .Where(b => b.Author.Name.ToLower().Contains(authorName.ToLower()))
                                   .ToListAsync();

        }

        public async Task<List<BookModel>> GetBooksByTitleAsync(string title)
        {
            return await _context.Books.Include(b => b.Author)
                              .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                              .ToListAsync();
        }

        public async Task<bool> DeleteBookByAuthorId(int id)
        {
            var author = await _context.Authors
                                        .Include(a => a.books)
                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return false;

            if (author.books.Any())
            {
                _context.Books.RemoveRange(author.books);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}