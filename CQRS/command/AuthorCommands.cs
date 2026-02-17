using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.command
{
    public static class AuthorCommands
    {
        public record CreateAuthorCommand(AuthorDTO Author) : IRequest<int>;
        public record DeleteAuthorByIdCommand(int Id) : IRequest<bool>;
        public record UpdateAuthorCommand(int Id, AuthorDTO Author) : IRequest<bool>;


    }
}