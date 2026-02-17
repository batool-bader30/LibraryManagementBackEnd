using LibraryManagement.Models;
using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.query
{
    public static class UserQueries
    {
        public record GetUserByIdQuery(string Id) : IRequest<UserModel?>;
        public record GetUserByUserNameQuery(string UserName) : IRequest<UserModel?>;
        public record GetAllUsersQuery() : IRequest<List<UserModel>>;
    }
}
