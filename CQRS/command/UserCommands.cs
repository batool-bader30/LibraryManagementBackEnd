using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using MediatR;

namespace LibraryManagement.CQRS.Command
{
    public class UserCommands
    {
        public record RegisterUserCommand(RegisterDto User) : IRequest<int>;
        public record LoginUserCommand(LogInDto Login) : IRequest<LoginResultDto>;


    }
}