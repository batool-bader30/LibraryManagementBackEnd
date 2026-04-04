using LibraryManagement.Models;
using LibraryManagement.DTO;
using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.CQRS
{
    public static class ReviewQueries
    {
        public record GetAllReviewsQuery() : IRequest<List<ReviewDTO>>;
        public record GetReviewByIdQuery(int Id) : IRequest<ReviewDTO?>;
        public record GetReviewsByBookIdQuery(int BookId) : IRequest<List<ReviewDTO>>;
        public record GetReviewsByUserIdQuery(string UserId) : IRequest<List<ReviewDTO>>;
    }
}