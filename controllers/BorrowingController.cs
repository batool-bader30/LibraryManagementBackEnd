using LibraryManagement.command;
using LibraryManagement.CQRS.Query;
using LibraryManagement.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LibraryManagement.CQRS.Query.ReviewQueries;

namespace LibraryManagement.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BorrowingController(IMediator mediator) => _mediator = mediator;

        [HttpGet("GetAllBorrowings")]
        public async Task<IActionResult> GetAllBorrowings()
        {
            var result = await _mediator.Send(new GetAllBorrowingsQuery());
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetBorrowingById/{id}")]
        public async Task<IActionResult> GetBorrowingById(int id)
        {
            var result = await _mediator.Send(new GetBorrowingByIdQuery(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("GetBorrowingsByUser/{userId}")]
        public async Task<IActionResult> GetBorrowingsByUser(string userId)
        {
            var result = await _mediator.Send(new GetBorrowingsByUserIdQuery(userId));
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpPost("CreateBorrowing")]
        public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // إرسال الطلب للـ Handler
                var id = await _mediator.Send(new BorrowingCommands.CreateBorrowingCommand(dto));
                return Ok(new { Message = "Borrowing created successfully", BorrowingId = id });
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {
                // هنا نمسك خطأ "الكتاب غير متاح" ونرجعه كـ BadRequest مع الرسالة
                return BadRequest(new { Error = ex.Message });
            }
            catch (System.Exception ex)
            {
                // لأي أخطاء أخرى غير متوقعة
                return StatusCode(500, new { Error = "An unexpected error occurred", Details = ex.Message });
            }
        }

        [HttpPut("UpdateBorrowing/{id}")]
        public async Task<IActionResult> UpdateBorrowing(int id, [FromBody] UpdateBorrowingDTO dto)
        {
            var result = await _mediator.Send(new BorrowingCommands.UpdateBorrowingCommand(id, dto));
            return result ? Ok("Updated") : NotFound();
        }

        [HttpDelete("DeleteBorrowing/{id}")]
        public async Task<IActionResult> DeleteBorrowing(int id)
        {
            var result = await _mediator.Send(new BorrowingCommands.DeleteBorrowingByIdCommand(id));
            return result ? Ok("Deleted") : NotFound();
        }
    }
}