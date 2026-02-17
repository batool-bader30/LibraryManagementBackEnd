using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.CQRS.Query
{
    public static class BorrowingQueries
    {
        public record GetAllBorrowingsQuery() : IRequest<List<BorrowingModel>>;
        public record GetBorrowingByIdQuery(int Id) : IRequest<BorrowingModel?>;
        public record GetActiveBorrowingsQuery() : IRequest<List<BorrowingModel>>;
        public record GetBorrowingsByUserIdQuery(int UserId) : IRequest<List<BorrowingModel>>;
    }
}