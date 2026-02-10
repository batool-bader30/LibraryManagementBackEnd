using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using LibraryManagement.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static LibraryManagement.command.AuthorCommands;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.controllers
{
    [ApiController]
    [Route("api")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ======================
        // GET AUTHORS BY NAME
        // ====================== 
        [HttpGet("GetAuthorsByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _mediator.Send(new GetAuthorsByNameQuery(name));

            if (result.Count == 0)
                return NotFound("Author not found");

            return Ok(result);
        }
        // ======================
        // GET ALL AUTHORS
        // ====================== 
        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _mediator.Send(new GetAllAuthorsQuery());

            if (result.Count == 0)
                return NotFound("No Authors found");

            return Ok(result);
        }
        // ======================
        // CREATE AUTHORS
        // ======================
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            if (string.IsNullOrWhiteSpace(authorDto.Name))
                return BadRequest("Author name is required");

            try
            {
                var result = await _mediator.Send(new CreateAuthorCommand(authorDto));
                return Ok(new { AuthorId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ======================
        // DELETE AUTHORS BY ID
        // ======================
        [HttpDelete("DeleteAuthorById/{id}")]
        public async Task<IActionResult> DeleteAuthorById(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorByIdCommand(id));

            if (!result)
                return NotFound("Author not found");

            return Ok("Author deleted successfully");
        }


        
    }
}