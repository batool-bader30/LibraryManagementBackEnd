using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.data
{
    public class AppDBcontext : IdentityDbContext<UserModel>
    {
        public AppDBcontext(DbContextOptions<AppDBcontext> options)
            : base(options)
        { }

        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<BorrowingModel> Borrowings { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<BookCategoryModel> BookCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Key for Many-to-Many
            modelBuilder.Entity<BookCategoryModel>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategoryModel>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategoryModel>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);
            // Book - Borrowings
            modelBuilder.Entity<BorrowingModel>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Borrowings)
                .HasForeignKey(b => b.BookId);

            // Book - Reviews
            modelBuilder.Entity<ReviewModel>()
                .HasOne(r => r.Book)
                .WithMany(bk => bk.Reviews)
                .HasForeignKey(r => r.BookId);

        }
    }
}
