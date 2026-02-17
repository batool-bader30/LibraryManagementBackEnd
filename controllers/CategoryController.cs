using System.ComponentModel.DataAnnotations;
using LibraryManagement.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LibraryManagement.command.CategoryCommands;
using static LibraryManagement.query.CategoryQueries;

namespace LibraryManagement.controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("GetCategoryByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _mediator.Send(new GetCategoryByNameQuery(name));
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {


            try
            {
                var id = await _mediator.Send(new CreateCategoryCommand(dto));
                return Ok(new { CategoryId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }




        [HttpDelete("DeleteCategoryById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryByIdCommand(id));
            return result ? Ok("Deleted") : NotFound();
        }
    }
}