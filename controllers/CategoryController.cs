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

        // ======================
        // GET ALL CATEGORIES
        // ====================== 
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAll()
        {
            // سيرجع List<CategoryDTO>
            var result = await _mediator.Send(new GetAllCategoriesQuery());

            if (result == null || !result.Any())
                return NotFound("No categories found.");

            return Ok(result);
        }

        // ======================
        // GET CATEGORY BY NAME
        // ====================== 
        [HttpGet("GetCategoryByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _mediator.Send(new GetCategoryByNameQuery(name));

            if (result == null || !result.Any())
                return NotFound($"No categories found with name: {name}");

            return Ok(result);
        }

        // ======================
        // CREATE CATEGORY
        // ======================
        [HttpPost("CreateCategory")]
        // [Authorize(Roles = "Admin")] // فكي التعليق عنها عند تفعيل نظام الحماية
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Category name is required.");

            try
            {
                var id = await _mediator.Send(new CreateCategoryCommand(dto));
                return Ok(new { Message = "Category created successfully", CategoryId = id });
            }
            catch (Exception ex)
            {
                // نرجع رسالة الخطأ (مثل: Category already exists)
                return BadRequest(new { Message = ex.Message });
            }
        }

        // ======================
        // UPDATE CATEGORY (جديد)
        // ======================
        [HttpPut("UpdateCategory/{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Category name is required.");

            var result = await _mediator.Send(new UpdateCategoryCommand(id, dto));

            if (!result)
                return NotFound($"Category with ID {id} not found.");

            return Ok(new { Message = "Category updated successfully" });
        }

        // ======================
        // DELETE CATEGORY BY ID
        // ======================
        [HttpDelete("DeleteCategoryById/{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryByIdCommand(id));

            if (!result)
                return NotFound($"Category with ID {id} not found.");

            return Ok(new { Message = "Category deleted successfully" });
        }
    }
}