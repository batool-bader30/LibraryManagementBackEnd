using LibraryManagement.DTO;
using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.CQRS.Query
{
    public static class ReviewQueries // تأكدي من الاسم إذا كان للمراجعات أو الاستعارة، هنا سأضع الخاص بالاستعارة
    {
        public record GetAllBorrowingsQuery() : IRequest<List<BorrowingResponseDTO>>;
        public record GetBorrowingByIdQuery(int Id) : IRequest<BorrowingResponseDTO?>;
        public record GetBorrowingsByUserIdQuery(string UserId) : IRequest<List<BorrowingResponseDTO>>;
        public record GetActiveBorrowingsQuery() : IRequest<List<BorrowingResponseDTO>>;
    }
}