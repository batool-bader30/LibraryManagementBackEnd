using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using LibraryManagement.query;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.CQRS.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<UserQueries.GetUserByIdQuery, UserModel?>
    {
        private readonly IUserRepository _repo;
        public GetUserByIdQueryHandler(IUserRepository repo) => _repo = repo;

        public async Task<UserModel?> Handle(UserQueries.GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetUserByIdAsync(request.Id);
        }
    }

    public class GetUserByUserNameQueryHandler : IRequestHandler<UserQueries.GetUserByUserNameQuery, UserModel?>
    {
        private readonly IUserRepository _repo;
        public GetUserByUserNameQueryHandler(IUserRepository repo) => _repo = repo;

        public async Task<UserModel?> Handle(UserQueries.GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetUserByUserNameAsync(request.UserName);
        }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<UserQueries.GetAllUsersQuery, List<UserModel>>
    {
        private readonly IUserRepository _repo;
        public GetAllUsersQueryHandler(IUserRepository repo) => _repo = repo;

        public async Task<List<UserModel>> Handle(UserQueries.GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllUsersAsync();
        }
    }
}
