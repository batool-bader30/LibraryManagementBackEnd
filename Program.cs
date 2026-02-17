using LibraryManagement.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MediatR;
using System.Reflection;
using LibraryManagement.Repositories.Interfaces;
using LibraryManagement.Repository.Implementation;
using LibraryManagement.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using LibraryManagement.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    );

// 2. Configure DbContext
builder.Services.AddDbContext<AppDBcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));

// 3. Swagger & JWT (إعداداتك الخاصة)
// 3. Swagger & JWT (إعداداتك الخاصة)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwtAuth();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5000",  
            ValidAudience = "http://localhost:5000",  
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lhkjdgdgdkmdfk2554354657knknjbn@#$%^&k4jh245"))  // مفتاح سري قوي (32 حرفاً على الأقل)
        };
    });
// 4. MediatR
builder.Services.AddMediatR(typeof(Program));

// 5. Repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



// 6. Identity Configuration
builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    // يمكنك تعديل شروط الباسورد هنا لتسهيل التيست
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDBcontext>()
    .AddDefaultTokenProviders();
    builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});


// 7. الحل الجذري لمشكلة الـ 404 (منع التحويل لصفحة Login)
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();
    
    // قائمة الأدوار المطلوبة
    string[] roles = { "User", "Admin" };
    
    foreach (var role in roles)
    {
        // تحقق إذا كان الدور موجوداً، وإلا أنشئه
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// 8. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();        
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
