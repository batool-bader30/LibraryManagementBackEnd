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
            // ضروري جداً لاستمرار عمل IdentityUser
            base.OnModelCreating(modelBuilder);

            // 1. إعداد المفتاح المركب لعلاقة (الكتب - التصنيفات)
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

            // 2. علاقة الكتاب مع الاستعارات
            modelBuilder.Entity<BorrowingModel>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Borrowings)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade); // إذا انحذف الكتاب تنحذف سجلات استعارته

            // 3. علاقة الكتاب مع المراجعات
            modelBuilder.Entity<ReviewModel>()
                .HasOne(r => r.Book)
                .WithMany(bk => bk.Reviews)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // 4. حماية المستخدم (Restrict Delete) 
            // لمنع حذف المستخدم إذا كان لديه سجلات استعارة أو مراجعات نشطة
            modelBuilder.Entity<BorrowingModel>()
                .HasOne(b => b.User)
                .WithMany(u => u.Borrowings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReviewModel>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // 5. علاقة المؤلف مع الكتب (One-to-Many)
            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.Author)
                .WithMany(a => a.books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}