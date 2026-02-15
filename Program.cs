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
using LibraryManagement.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext
builder.Services.AddDbContext<AppDBcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));

// Swagger & JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwtAuth();
builder.Services.AddCustomJwtAuth(builder.Configuration);

// MediatR
builder.Services.AddMediatR(typeof(Program));

// Repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Identity
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<AppDBcontext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Static files
app.UseStaticFiles();

// HTTPS redirection
app.UseHttpsRedirection();


// Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
