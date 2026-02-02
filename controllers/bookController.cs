using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.data;
using LibraryManagement.DTO;
using LibraryManagement.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly addDBcontext _context;

        public BookController(addDBcontext context)
        {
            _context = context;
        }

        // ============================
        // POST: Add Book with Image
        // ============================
        [HttpPost("postbook")]
        public async Task<IActionResult> PostBook([FromForm] CreateBookDto book)
        {
            if (await _context.Books.AnyAsync(b => b.ISBN == book.ISBN))
                return BadRequest("Book with this ISBN already exists!");

            if (!await _context.Authors.AnyAsync(a => a.Id == book.AuthorId))
                return BadRequest("Author not found!");

            var newBook = new Bookmodel
            {
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                PublishYear = book.PublishYear,
                ISBN = book.ISBN,
                AuthorId = book.AuthorId
            };

            if (book.ImageUrl != null)
            {
                using var ms = new MemoryStream();
                await book.ImageUrl.CopyToAsync(ms);
                newBook.ImageUrl = ms.ToArray();
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                newBook.Id,
                newBook.Title
            });
        }

        // ============================
        // GET: All Books (with image URL)
        // ============================
        [HttpGet("getbooks")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Books
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    PublishYear = b.PublishYear,
                    ISBN = b.ISBN,
                    AuthorId = b.AuthorId,
                    ImageUrl = b.ImageUrl != null
                        ? $"{Request.Scheme}://{Request.Host}/api/book/{b.Id}/image"
                        : null
                })
                .ToListAsync();

            if (!books.Any())
                return NotFound("No books exist!");

            return Ok(books);
        }


        // ============================
        // GET: Book Image
        // ============================
        [HttpGet("{id}/image")]
        public IActionResult GetBookImage(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null || book.ImageUrl == null)
                return NotFound();

            return File(book.ImageUrl, "image/*");
        }

        // ============================
        // GET: Books by Name
        // ============================
        [HttpGet("GetBooksByName")]
        public async Task<IActionResult> GetBooksByName(string name)
        {
            var books = await _context.Books
                .Where(b => b.Title == name)
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    PublishYear = b.PublishYear,
                    ISBN = b.ISBN,
                    AuthorId = b.AuthorId,
                    ImageUrl = b.ImageUrl != null
                        ? $"{Request.Scheme}://{Request.Host}/api/book/{b.Id}/image"
                        : null
                })
                .ToListAsync();

            if (!books.Any())
                return NotFound($"{name} not found!");

            return Ok(books);
        }

        // ============================
        // GET: Books by Name
        // ============================
        [HttpGet("GetBooksByAuthorName")]
        public async Task<IActionResult> GetBooksByAuthorName(string name)
        {
            var books = await _context.Books
                .Where(b => b.Author.Name.Contains(name))
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    PublishYear = b.PublishYear,
                    ISBN = b.ISBN,
                    AuthorId = b.AuthorId,
                    ImageUrl = b.ImageUrl != null
                        ? $"{Request.Scheme}://{Request.Host}/api/book/{b.Id}/image"
                        : null
                })
                .ToListAsync();

            if (!books.Any())
                return NotFound($"{name} not found!");

            return Ok(books);
        }

        // ============================
        // DELETE: Book by Id
        // ============================
        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound("Book Not Found");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok($"{book.Title} was deleted");
        }
    }
}
