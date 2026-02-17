using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.CQRS.Query
{
    public static class ReviewQueries
    {
        public record GetAllReviewsQuery() : IRequest<List<ReviewModel>>;
        public record GetReviewByIdQuery(int Id) : IRequest<ReviewModel?>;
        public record GetReviewsByBookIdQuery(int BookId) : IRequest<List<ReviewModel>>;
        public record GetReviewsByUserIdQuery(int UserId) : IRequest<List<ReviewModel>>;
    }
}