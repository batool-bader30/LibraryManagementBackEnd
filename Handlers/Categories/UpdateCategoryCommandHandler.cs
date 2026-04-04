using MediatR;
using static LibraryManagement.command.CategoryCommands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _repo;
    public UpdateCategoryCommandHandler(ICategoryRepository repo) => _repo = repo;

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken ct)
    {
        var category = await _repo.GetCategoryByIdAsync(request.Id);
        if (category == null) return false;

        category.Name = request.Category.Name;
        await _repo.UpdateCategoryAsync(category);
        return true;
    }
}