using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.CQRS.command;
using LibraryManagement.query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LibraryManagement.CQRS.command.BookCommands;

namespace LibraryManagement.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public BookController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _mediator.Send(new BookQueries.GetAllBooksQuery());
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _mediator.Send(new BookQueries.GetBookByIdQuery(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("GetBooksByAuthor/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            var result = await _mediator.Send(new BookQueries.GetBooksByAuthorQuery(authorId));
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetBooksByCategory/{categoryId}")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var result = await _mediator.Send(new BookQueries.GetBooksByCategoryQuery(categoryId));
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetAvailableBooks")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            var result = await _mediator.Send(new BookQueries.GetAvailableBooksQuery());
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromForm] CreateBookDTO dto)
        {
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                // إنشاء اسم فريد للملف
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);

                // مسار الفولدر النهائي
                var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "books");

                // إنشاء الفولدر إذا ما موجود
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // مسار الملف الكامل
                var filePath = Path.Combine(folderPath, fileName);

                // حفظ الملف
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ImageFile.CopyToAsync(stream);

                // رابط الصورة ليُحفظ في قاعدة البيانات
                dto.ImageUrl = "/images/books/" + fileName;
            }

            var bookId = await _mediator.Send(new CreateBookCommand(dto));
            return Ok(new { BookId = bookId });
        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] BookDTO dto)
        {
            // إذا تم رفع صورة جديدة أثناء التحديث
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "books");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ImageFile.CopyToAsync(stream);

                dto.ImageUrl = "/images/books/" + fileName;
            }

            var result = await _mediator.Send(new BookCommands.UpdateBookCommand(id, dto));
            return result ? Ok("Updated") : NotFound();
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new BookCommands.DeleteBookByIdCommand(id));
            return result ? Ok("Deleted") : NotFound();
        }
    }
}
