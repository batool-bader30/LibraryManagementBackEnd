using LibraryManagement.DTO;
using LibraryManagement.Models;
using MediatR;

namespace LibraryManagement.command
{
    public static class UserCommands
    {
        public record UpdateUserCommand(string Id, RegisterDto User) : IRequest<string>;
        public record DeleteUserCommand(string Id) : IRequest<bool>;
    }
}
