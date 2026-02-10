using LibraryManagement.models;
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
            var exist = await _repo.GetCategoryByNameAsync(request.Category.Name);
            if (exist.Any())
                throw new Exception("Category already exists");

            var category = new CategoryModel
            {
                Name = request.Category.Name
            };

            await _repo.CreateCategory(category);
            return category.Id;
        }
    }
}
