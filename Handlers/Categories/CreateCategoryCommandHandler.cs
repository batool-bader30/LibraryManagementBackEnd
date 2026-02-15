using LibraryManagement.Models;
using LibraryManagement.Repositories.Interfaces;
using MediatR;
using static LibraryManagement.command.CategoryCommands;

namespace LibraryManagement.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _repo;

        public CreateCategoryCommandHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            try
            {
                Console.WriteLine("Handler: Start handling CreateCategoryCommand");

                if (request.Category == null)
                {
                    Console.WriteLine("Handler Error: Category DTO is null");
                    throw new Exception("Category DTO is null");
                }

                Console.WriteLine($"Handler: DTO Name = {request.Category.Name}");

                var exist = await _repo.GetCategoryByNameAsync(request.Category.Name);
                if (exist.Any())
                {
                    Console.WriteLine("Handler Error: Category already exists");
                    throw new Exception("Category already exists");
                }

                var category = new CategoryModel
                {
                    Name = request.Category.Name
                };

                await _repo.CreateCategory(category);
                Console.WriteLine($"Handler: Category created with ID = {category.Id}");

                return category.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handler Exception: {ex.Message}");
                throw;
            }
        }
    }
}
