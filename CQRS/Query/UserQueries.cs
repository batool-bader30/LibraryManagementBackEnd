using LibraryManagement.DTO;
using MediatR;
using System.Collections.Generic;

namespace LibraryManagement.query
{
    public static class UserQueries
    {
        public record GetUserByIdQuery(string Id) : IRequest<UserSimpleDTO?>;
        public record GetAllUsersQuery() : IRequest<List<UserSimpleDTO>>;
        public record GetUserByUserNameQuery(string UserName) : IRequest<UserSimpleDTO?>;
    }
}