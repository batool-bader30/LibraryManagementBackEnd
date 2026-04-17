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
            modelBuilder.Entity<AuthorModel>().HasData(
  new AuthorModel
  {
      Id = 1,
      Name = "Stephen King",
      Bio = "An American author of horror, supernatural fiction, suspense, and fantasy novels. His books have sold more than 350 million copies, many of which have been adapted into films and miniseries.",
      ImageUrl = "https://images.gr-assets.com/authors/1362814142p8/3389.jpg"
  },
  new AuthorModel
  {
      Id = 2,
      Name = "Colleen Hoover",
      Bio = "A #1 New York Times bestselling author of many contemporary romance novels, including the blockbuster 'It Ends with Us'. She is known for her emotionally intense storytelling.",
      ImageUrl = "https://images.gr-assets.com/authors/1655301825p8/5027166.jpg"
  },
  new AuthorModel
  {
      Id = 3,
      Name = "Mark Manson",
      Bio = "An American self-help author and blogger. He is the author of the popular blog and the best-selling book 'The Subtle Art of Not Giving a F*ck', which redefined modern self-improvement.",
      ImageUrl = "https://images.gr-assets.com/authors/1473105785p8/14002905.jpg"
  },
  new AuthorModel
  {
      Id = 4,
      Name = "James Clear",
      Bio = "An American author and speaker focused on habits, decision-making, and continuous improvement. He is the author of the #1 New York Times bestseller 'Atomic Habits'.",
      ImageUrl = "https://images.gr-assets.com/authors/1517430405p8/7213443.jpg"
  }
);

            modelBuilder.Entity<BookModel>().HasData(
                new BookModel
                {
                    Id = 1,
                    Title = "It",
                    Description = "Seven helpless and abandoned children are searched by a killer who takes the shape of a clown. A masterpiece of horror and supernatural suspense.",
                    ISBN = "9781501142970",
                    AuthorId = 1,
                    PageNumber = "1138",
                    Language = "English",
                    PublishDate = "1986-09-15",
                    ImageUrl = "https://m.media-amazon.com/images/I/71dNjAs6KwL.jpg",
                    IsAvailable = true
                },
                new BookModel
                {
                    Id = 2,
                    Title = "Atomic Habits",
                    Description = "A revolutionary guide to using tiny changes to deliver remarkable results. It explains the science of how habits are formed and how to master them.",
                    ISBN = "9780735211292",
                    AuthorId = 4,
                    PageNumber = "320",
                    Language = "English",
                    PublishDate = "2018-10-16",
                    ImageUrl = "https://m.media-amazon.com/images/I/91bYsX41DVL.jpg",
                    IsAvailable = true
                },
                new BookModel
                {
                    Id = 3,
                    Title = "Verity",
                    Description = "Lowen Ashleigh is a struggling writer on the brink of financial ruin when she accepts the job offer of a lifetime: to complete the remaining books in a successful series.",
                    ISBN = "9781538724545",
                    AuthorId = 2,
                    PageNumber = "336",
                    Language = "English",
                    PublishDate = "2018-12-07",
                    ImageUrl = "https://m.media-amazon.com/images/I/41m9S2E8q0L._SY445_SX342_.jpg",
                    IsAvailable = true
                },
                new BookModel
                {
                    Id = 4,
                    Title = "The Subtle Art",
                    Description = "A practical guide to living a good life, not by trying to be happy all the time, but by becoming better at handling adversity and choosing what matters.",
                    ISBN = "9780062457714",
                    AuthorId = 3,
                    PageNumber = "224",
                    Language = "English",
                    PublishDate = "2016-09-13",
                    ImageUrl = "https://m.media-amazon.com/images/I/71QKQ9mwV7L.jpg",
                    IsAvailable = true
                }
            );
        }
    }
}