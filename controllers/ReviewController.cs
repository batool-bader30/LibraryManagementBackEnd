using LibraryManagement.Models;
using LibraryManagement.command;
using LibraryManagement.query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LibraryManagement.command.ReviewCommands;
using static LibraryManagement.CQRS.Query.ReviewQueries;
using LibraryManagement.DTO;

namespace LibraryManagement.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _mediator.Send(new GetAllReviewsQuery());
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetReviewById/{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var result = await _mediator.Send(new GetReviewByIdQuery(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("GetReviewsByBookId/{bookId}")]
        public async Task<IActionResult> GetReviewsByBookId(int bookId)
        {
            var result = await _mediator.Send(new GetReviewsByBookIdQuery(bookId));
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetReviewsByUserId/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            var result = await _mediator.Send(new GetReviewsByUserIdQuery(userId));
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewDTO review)
        {
            var id = await _mediator.Send(new CreateReviewCommand(review));
            return Ok(new { ReviewId = id });
        }

        [HttpDelete("DeleteReview/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _mediator.Send(new DeleteReviewByIdCommand(id));
            return result ? Ok("Deleted") : NotFound();
        }
    }
}
