namespace LibraryManagement.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateCategoryDTO
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryDTO
    {
        public string Name { get; set; }
    }

}
