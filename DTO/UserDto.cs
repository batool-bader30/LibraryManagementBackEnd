namespace LibraryManagement.DTO
{
    public class UserSimpleDTO
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } // إضافة هذا السطر
    }
    public class UpdateUserDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password{get; set;}
        public string? PhoneNumber { get; set; } // إضافة هذا السطر
    }

}