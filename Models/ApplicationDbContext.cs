
namespace DisCourse.Models
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // 🔥 THÊM DÒNG NÀY
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Course)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa tự động

            // Ngăn chặn ON DELETE CASCADE ở khóa ngoại OwnerID
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerID)
                .OnDelete(DeleteBehavior.Restrict); // ⚠️ Đổi từ Cascade → Restrict
        }

    }
}
