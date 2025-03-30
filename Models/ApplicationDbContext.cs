using Microsoft.EntityFrameworkCore;

namespace DisCourse.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasData(new Course
                {
                    Id = 1,
                    Name = "Không chủ đề",
                    Description = "Các bài viết không thuộc chủ đề cụ thể",
                    CreatedAt = new DateTime(2025, 3, 29, 0, 0, 0, DateTimeKind.Utc) // Giá trị cố định
                });

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Course)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
