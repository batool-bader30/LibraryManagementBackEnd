using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.data
{
    public class AppDBcontext
        : IdentityDbContext<UserModel>
    {
        public AppDBcontext(DbContextOptions<AppDBcontext> options)
         : base(options)
        { }
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

    }
}