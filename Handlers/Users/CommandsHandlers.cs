using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using LibraryManagement.command;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.CQRS.Handlers
{
   
    public class UpdateUserCommandHandler : IRequestHandler<UserCommands.UpdateUserCommand, string>
    {
        private readonly IUserRepository _repo;

        public UpdateUserCommandHandler(IUserRepository repo) => _repo = repo;

        public async Task<string> Handle(UserCommands.UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.User.userName))
                return "Username is required";

            var user = new UserModel
            {
                Id = request.Id,
                UserName = request.User.userName,
                Email = request.User.email
            };

            return await _repo.UpdateUserAsync(user);
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<UserCommands.DeleteUserCommand, bool>
    {
        private readonly IUserRepository _repo;

        public DeleteUserCommandHandler(IUserRepository repo) => _repo = repo;

        public async Task<bool> Handle(UserCommands.DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteUserAsync(request.Id);
        }
    }
}
