using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.command
{
     public static class BorrowingCommands
    {
        public record CreateBorrowingCommand(BorrowingDTO Borrowing) : IRequest<int>;
        public record DeleteBorrowingByIdCommand(int Id) : IRequest<bool>;
        public record UpdateBorrowingCommand(int Id, BorrowingDTO Borrowing) : IRequest<bool>;
    }
}