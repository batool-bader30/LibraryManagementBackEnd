using LibraryManagement.Models;
using LibraryManagement.command;
using LibraryManagement.query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static LibraryManagement.command.BorrowingCommands;
using static LibraryManagement.CQRS.Query.BorrowingQueries;
using LibraryManagement.DTO;

namespace LibraryManagement.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BorrowingController(IMediator mediator)
        {
            _mediator = mediator;
        }

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
        public async Task<IActionResult> GetBorrowingsByUser(int userId)
        {
            var result = await _mediator.Send(new GetBorrowingsByUserIdQuery(userId));
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetActiveBorrowings")]
        public async Task<IActionResult> GetActiveBorrowings()
        {
            var result = await _mediator.Send(new GetActiveBorrowingsQuery());
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpPost("CreateBorrowing")]
        public async Task<IActionResult> CreateBorrowing(BorrowingDTO borrowing)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var id = await _mediator.Send(new CreateBorrowingCommand(borrowing));
            return Ok(new { BorrowingId = id });
        }

        [HttpPut("UpdateBorrowing/{id}")]
        public async Task<IActionResult> UpdateBorrowing(int id, BorrowingDTO borrowing)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _mediator.Send(new UpdateBorrowingCommand(id, borrowing));
            return result ? Ok("Updated") : NotFound();
        }

        [HttpDelete("DeleteBorrowing/{id}")]
        public async Task<IActionResult> DeleteBorrowing(int id)
        {
            var result = await _mediator.Send(new DeleteBorrowingByIdCommand(id));
            return result ? Ok("Deleted") : NotFound();
        }
    }
}
