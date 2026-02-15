// using MediatR;
// using Microsoft.AspNetCore.Identity;
// using System.Threading;
// using System.Threading.Tasks;
// using LibraryManagement.CQRS.Command;
// using LibraryManagement.Models;

// public class RegisterUserCommandHandler : IRequestHandler<UserCommands.RegisterUserCommand, int>
// {
//     private readonly UserManager<UserModel> _userManager;
//     private readonly RoleManager<IdentityRole> _roleManager; // إضافة RoleManager

//     public RegisterUserCommandHandler(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
//     {
//         _userManager = userManager;
//         _roleManager = roleManager; // حفظه لاستخدامهس
//     }

//     public async Task<int> Handle(UserCommands.RegisterUserCommand request, CancellationToken cancellationToken)
//     {
//         var user = new UserModel
//         {
//             UserName = request.User.UserName,
//             Email = request.User.Email,
//             PhoneNumber = request.User.PhoneNumber
//         };

//         var result = await _userManager.CreateAsync(user, request.User.Password);

//         if (result.Succeeded)
//         {
//             // إضافة الدور
//             if (!string.IsNullOrEmpty(request.User.Role))
//             {
//                 // تأكدي أن الدور موجود في DB
//                 var roleExists = await _roleManager.RoleExistsAsync(request.User.Role);
//                 if (!roleExists)
//                 {
//                     await _roleManager.CreateAsync(new IdentityRole(request.User.Role));
//                 }
//                 await _userManager.AddToRoleAsync(user, request.User.Role);
//             }

//             return int.TryParse(user.Id, out var id) ? id : 1;
//         }

//         return 0; // يدل على فشل التسجيل
//     }
// }
