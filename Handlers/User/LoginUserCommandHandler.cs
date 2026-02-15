// using LibraryManagement.DTO;
// using LibraryManagement.Models;
// using MediatR;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using static LibraryManagement.CQRS.Command.UserCommands;

// namespace LibraryManagement.Handlers.User
// {
//     public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResultDto>
//     {
//         private readonly UserManager<UserModel> _userManager;
//         private readonly IConfiguration _configuration;

//         public LoginUserCommandHandler(UserManager<UserModel> userManager, IConfiguration configuration)
//         {
//             _userManager = userManager;
//             _configuration = configuration;
//         }

//         public async Task<LoginResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
//         {
//             // البحث عن المستخدم
//             var user = await _userManager.FindByNameAsync(request.Login.UserName);
//             if (user == null)
//                 return new LoginResultDto { Success = false, Message = "User not found" };

//             // التحقق من كلمة السر
//             if (!await _userManager.CheckPasswordAsync(user, request.Login.Password))
//                 return new LoginResultDto { Success = false, Message = "Invalid password" };

//             // إنشاء Claims
//             var claims = new List<Claim>
//             {
//                 new Claim(ClaimTypes.Name, user.UserName),
//                 new Claim(ClaimTypes.NameIdentifier, user.Id),
//                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//             };

//             // إضافة الدور (Role) الصحيح
//             var roles = await _userManager.GetRolesAsync(user);
//             var role = roles.FirstOrDefault() ?? "User"; // نأخذ أول دور أو User افتراضي
//             claims.Add(new Claim(ClaimTypes.Role, role));

//             // إنشاء JWT
//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             var token = new JwtSecurityToken(
//                 issuer: _configuration["JWT:Issuer"],
//                 audience: _configuration["JWT:Audience"],
//                 claims: claims,
//                 expires: DateTime.Now.AddHours(1),
//                 signingCredentials: creds
//             );

//             // إعادة الـ Token مع المعلومات
//             return new LoginResultDto
//             {
//                 Success = true,
//                 Token = new JwtSecurityTokenHandler().WriteToken(token),
//                 Expiration = token.ValidTo,
//                 Role = role
//             };
//         }
//     }
// }
