using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.DTO;
using LibraryManagement.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        // GET AUTHORS BY NAME
        // ====================== 
        [HttpGet("GetAuthorsByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try 
            {
                // الـ Result الآن هو List<AuthorDTO>
                var result = await _mediator.Send(new GetAuthorsByNameQuery(name));
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // ======================
        // GET ALL AUTHORS
        // ====================== 
        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                // الـ Result هو List<AuthorDTO>
                var result = await _mediator.Send(new GetAllAuthorsQuery());
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // ======================
        // CREATE AUTHORS
        // ======================
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO AuthorDTO)
        {
            if (string.IsNullOrWhiteSpace(AuthorDTO.Name))
                return BadRequest("Author name is required");

            try
            {
                // التعديل المهم: الـ result الآن هو AuthorResponseDTO كامل
                var result = await _mediator.Send(new CreateAuthorCommand(AuthorDTO));
                
                // نرجع الكائن كامل لـ Flutter عشان يقدر يعرض الاسم والـ ID فوراً
                return Ok(result); 
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