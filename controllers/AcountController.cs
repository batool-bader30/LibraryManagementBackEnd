using LibraryManagement.DTO;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly string _secretKey = "lhkjdgdgdkmdfk2554354657knknjbn@#$%^&k4jh245";  // نفس المفتاح المستخدم في الإعداد
        private readonly string _issuer = "http://localhost:5000";
        private readonly string _audience = "http://localhost:5000";

        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // إنشاء المستخدم الجديد
            var user = new UserModel
            {
                UserName = model.userName,
                Email = model.userName  // افترض أن userName هو البريد الإلكتروني، أو أضف حقل Email في DTO إذا لزم
            };

            var result = await _userManager.CreateAsync(user, model.password);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "فشل في إنشاء الحساب", errors = result.Errors });
            }

            // إضافة الدور (إذا كان Admin، تأكد من الصلاحية)
            if (!string.IsNullOrEmpty(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            // إنشاء توكن JWT بعد التسجيل (اختياري)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, model.Role ?? "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new { message = "تم التسجيل بنجاح", token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResultDto { Success = false, Message = "بيانات غير صحيحة" });
            }

            // محاولة تسجيل الدخول
            var result = await _signInManager.PasswordSignInAsync(model.userName, model.password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new LoginResultDto { Success = false, Message = "بيانات الدخول غير صحيحة" });
            }

            // الحصول على المستخدم لاستخراج الدور
            var user = await _userManager.FindByNameAsync(model.userName);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";  // افترض دور واحد

            // إنشاء توكن JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return Ok(new LoginResultDto
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "تم تسجيل الدخول بنجاح",
                Role = role
            });
        }
    }
}