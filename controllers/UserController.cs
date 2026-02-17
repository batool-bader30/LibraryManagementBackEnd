using LibraryManagement.DTO;
using LibraryManagement.command;
using LibraryManagement.query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LibraryManagement.command.UserCommands;
using static LibraryManagement.query.UserQueries;

namespace LibraryManagement.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new UserQueries.GetAllUsersQuery());
            return users.Count == 0 ? NotFound("No users found") : Ok(users);
        }

       
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return user == null ? NotFound("User not found") : Ok(user);
        }

  
        [HttpGet("GetUserByUserName/{username}")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var user = await _mediator.Send(new UserQueries.GetUserByUserNameQuery(username));
            return user == null ? NotFound("User not found") : Ok(user);
        }

        
       
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(new UpdateUserCommand(id, dto));
            return Ok(new { Message = result });
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deleted = await _mediator.Send(new DeleteUserCommand(id));
            return deleted ? Ok("User deleted successfully") : NotFound("User not found");
        }
    }
}
