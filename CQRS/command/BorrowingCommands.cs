using LibraryManagement.DTO;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.command
{
    public static class BorrowingCommands
    {
        public record CreateBorrowingCommand(CreateBorrowingDTO Borrowing) : IRequest<int>;
        public record DeleteBorrowingByIdCommand(int Id) : IRequest<bool>;
        public record UpdateBorrowingCommand(int Id, UpdateBorrowingDTO Borrowing) : IRequest<bool>;
    }
}