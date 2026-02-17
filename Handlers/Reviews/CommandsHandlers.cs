using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static LibraryManagement.command.ReviewCommands;

namespace LibraryManagement.command
{
    public class ReviewCommandHandlers
    {
        public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, int>
        {
            private readonly IReviewRepository _repo;

            public CreateReviewHandler(IReviewRepository repo)
            {
                _repo = repo;
            }

            public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.Review.Comment))
                    throw new ValidationException("Comment cannot be empty.");

                if (request.Review.Rating < 1 || request.Review.Rating > 5)
                    throw new ValidationException("Rating must be between 1 and 5.");
                var review = new ReviewModel
                {
                    BookId = request.Review.BookId,
                    UserId = request.Review.UserId.ToString(),
                    Comment = request.Review.Comment,
                    Rating = request.Review.Rating
                };
                await _repo.AddReviewAsync(review);
                return request.Review.Id;
            }
        }

        public class DeleteReviewHandler : IRequestHandler<DeleteReviewByIdCommand, bool>
        {
            private readonly IReviewRepository _repo;

            public DeleteReviewHandler(IReviewRepository repo)
            {
                _repo = repo;
            }

            public async Task<bool> Handle(DeleteReviewByIdCommand request, CancellationToken cancellationToken)
            {
                return await _repo.DeleteReviewAsync(request.Id);
            }
        }
    }
}
