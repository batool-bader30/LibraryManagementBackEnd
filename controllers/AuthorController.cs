using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static LibraryManagement.command.AuthorCommands;
using static LibraryManagement.query.AuthorQuerys;

namespace LibraryManagement.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ======================
        // GET ALL AUTHORS
        // ====================== 
        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var result = await _mediator.Send(new GetAllAuthorsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ======================
        // GET AUTHORS BY NAME
        // ====================== 
        [HttpGet("GetAuthorsByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try 
            {
                var result = await _mediator.Send(new GetAuthorsByNameQuery(name));
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // ======================
        // CREATE AUTHOR
        // ======================
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO AuthorDTO)
        {
            // التحقق من صحة البيانات (Validation)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // الـ AuthorDTO الآن يحتوي على ImageUrl وسيمرره الـ Mediator للـ Handler
                var result = await _mediator.Send(new CreateAuthorCommand(AuthorDTO));
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===================================
        // UPDATE AUTHOR (إضافة الـ Action المفقود)
        // ===================================
        [HttpPut("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDTO AuthorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _mediator.Send(new UpdateAuthorCommand(id, AuthorDTO));
                
                if (!result)
                    return NotFound($"Author with ID {id} not found");

                return Ok(new { message = "Author updated successfully" });
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

            return Ok(new { message = "Author deleted successfully" });
        }
    }
}