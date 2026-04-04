using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    public string userName { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string email { get; set; }
    public string? phoneNumber { get; set; }
    public string Role { get; set; } = "User";
}

public class LogInDto
{
    [Required]
    public string userName { get; set; }
    [Required]
    public string password { get; set; }
}