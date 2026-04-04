using LibraryManagement.command;
using LibraryManagement.CQRS.Query;
using LibraryManagement.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LibraryManagement.CQRS.ReviewQueries;

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

        // --- Queries (الاستعلامات) ---

        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _mediator.Send(new GetAllReviewsQuery());
            return result == null || result.Count == 0 ? NotFound("No reviews found") : Ok(result);
        }

        [HttpGet("GetReviewById/{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var result = await _mediator.Send(new GetReviewByIdQuery(id));
            return result == null ? NotFound($"Review with ID {id} not found") : Ok(result);
        }

        [HttpGet("GetReviewsByBookId/{bookId}")]
        public async Task<IActionResult> GetReviewsByBookId(int bookId)
        {
            var result = await _mediator.Send(new GetReviewsByBookIdQuery(bookId));
            return result == null || result.Count == 0 ? NotFound("No reviews for this book") : Ok(result);
        }

        [HttpGet("GetReviewsByUserId/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(string userId) // تم التغيير لـ string لتوافق الـ Identity
        {
            var result = await _mediator.Send(new GetReviewsByUserIdQuery(userId));
            return result == null || result.Count == 0 ? NotFound("This user has no reviews") : Ok(result);
        }

        // --- Commands (الأوامر) ---

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO reviewDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var id = await _mediator.Send(new ReviewCommands.CreateReviewCommand(reviewDto));
                return Ok(new { Message = "Review added successfully", ReviewId = id });
            }
            catch (Exception ex)
            {
                // سيمسك الأخطاء مثل (الكتاب غير موجود أو المستخدم غير موجود) القادمة من الـ Repository
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("UpdateReview/{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewDTO updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _mediator.Send(new ReviewCommands.UpdateReviewCommand(id, updateDto));
                return result ? Ok(new { Message = "Review updated successfully" }) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("DeleteReview/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _mediator.Send(new ReviewCommands.DeleteReviewByIdCommand(id));
            return result ? Ok(new { Message = "Review deleted" }) : NotFound("Review not found");
        }
    }
}