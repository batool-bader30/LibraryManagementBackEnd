
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace LibraryManagement.Models
{
    public class UserModel : IdentityUser
    {
public ICollection<BorrowingModel> Borrowings { get; set; }
    public ICollection<ReviewModel> Reviews { get; set; }
    }
}
