using LibraryManagement.DTO;
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
            // سيرجع List<BookSimpleDTO>
            var result = await _mediator.Send(new BookQueries.GetAllBooksQuery());
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            // سيرجع BookDetailedDTO
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
                dto.ImageUrl = await SaveImage(dto.ImageFile);
            }

            var bookId = await _mediator.Send(new CreateBookCommand(dto));
            return Ok(new { BookId = bookId });
        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] UpdateBookDTO dto)
        {
            // التأكد من تطابق الـ ID
            dto.Id = id;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                dto.ImageUrl = await SaveImage(dto.ImageFile);
            }

            // إرسال الـ UpdateBookDTO للـ Handler
            var result = await _mediator.Send(new BookCommands.UpdateBookCommand(dto));
            return result ? Ok("Updated") : NotFound();
        }

        [HttpDelete("DeleteBook/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new BookCommands.DeleteBookByIdCommand(id));
            return result ? Ok("Deleted") : NotFound();
        }

        // ميثود مساعدة (Private) لتقليل تكرار كود حفظ الصور
        private async Task<string> SaveImage(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "images", "books");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return "/images/books/" + fileName;
        }
    }
}