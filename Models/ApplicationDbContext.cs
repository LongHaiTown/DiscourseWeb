
namespace DisCourse.Models
{
    using DisCourseW.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }
        
        public DbSet<UserProfilePicture> UserProfilePictures { get; set; } // Thêm bảng UserProfilePicture
        public DbSet<LikePost> LikePosts { get; set; } // Thêm bảng UserProfilePicture

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Course)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfilePicture>()
                .HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<LikePost>()
                .HasOne(lp => lp.User)
                .WithMany() // Nếu bạn không muốn có một collection cho User
                .HasForeignKey(lp => lp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LikePost>()
                .HasOne(lp => lp.Post)
                .WithMany(p => p.Likes) // Thay đổi ở đây để liên kết với collection Likes trong Post
                .HasForeignKey(lp => lp.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Thay đổi thành Cascade nếu bạn muốn tự động xóa lượt thích khi bài viết bị xóa
        }

    }
}
