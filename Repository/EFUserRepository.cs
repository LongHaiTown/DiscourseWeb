using DisCourse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DisCourseW.Repository
{
    public class EFUserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public EFUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả user trong hệ thống
        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Id == userId);
        }
                // Lấy tất cả khóa học của user theo OwnerID
        public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId)
        {
            return await _context.Courses
                .Where(course => course.OwnerID == userId)
                .ToListAsync();
        }

        public Task<IEnumerable<(IdentityUser User, int CourseCount)>> GetUsersWithCourseCountAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsByOwner(string userId)
        {
            // LINQ truy vấn lấy bài viết thuộc về các khóa học mà user sở hữu
            var posts = _context.Courses
                .Where(course => course.OwnerID == userId) // Lọc khóa học theo OwnerID
                .SelectMany(course => course.Posts)        // Lấy tất cả bài viết từ các khóa học
                .Include(post => post.Author)             // Bao gồm thông tin tác giả bài viết
                .Include(post => post.Course)             // Bao gồm thông tin khóa học
                .ToList();

            return posts;
        }


    }
}
