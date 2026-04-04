using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.DTO;
using MediatR;
using LibraryManagement.query;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.CQRS.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<UserQueries.GetUserByIdQuery, UserSimpleDTO?>
    {
        private readonly IUserRepository _repo;
        public GetUserByIdQueryHandler(IUserRepository repo) => _repo = repo;

        public async Task<UserSimpleDTO?> Handle(UserQueries.GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetUserByIdAsync(request.Id);
            if (user == null) return null;

            return new UserSimpleDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }

    public class GetUserByUserNameQueryHandler : IRequestHandler<UserQueries.GetUserByUserNameQuery, UserSimpleDTO?>
    {
        private readonly IUserRepository _repo;
        public GetUserByUserNameQueryHandler(IUserRepository repo) => _repo = repo;

        public async Task<UserSimpleDTO?> Handle(UserQueries.GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetUserByUserNameAsync(request.UserName);
            if (user == null) return null;

            return new UserSimpleDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<UserQueries.GetAllUsersQuery, List<UserSimpleDTO>>
    {
        private readonly IUserRepository _repo;
        public GetAllUsersQueryHandler(IUserRepository repo) => _repo = repo;

        public async Task<List<UserSimpleDTO>> Handle(UserQueries.GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.GetAllUsersAsync();
            return users.Select(u => new UserSimpleDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ToList();
        }
    }
}