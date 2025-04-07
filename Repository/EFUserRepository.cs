using DisCourse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DisCourseW.Repository
{
    public class EFUserRepository : IUserRepository
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

        // Phương thức mới để lấy ảnh đại diện của người dùng
        public async Task<string> GetUserProfilePictureAsync(string userId)
        {
            var userProfilePicture = await _context.UserProfilePictures
                .FirstOrDefaultAsync(picture => picture.UserId == userId);

            return userProfilePicture?.ProfilePictureUrl; // Trả về URL ảnh đại diện hoặc null nếu không tìm thấy
        }

        public async Task<IEnumerable<Course>> GetCoursesByUserAsync(string userId)
        {
            // Lấy danh sách các khóa học mà user đã đăng ký từ bảng liên kết UserCourses
            return await _context.UserCourses
                .Where(uc => uc.UserId == userId) // Lọc theo UserId
                .Select(uc => uc.Course)         // Lấy thông tin khóa học từ liên kết
                .ToListAsync();                  // Chuyển đổi sang danh sách
        }
        public async Task<List<IdentityUser>> GetUsersByRoleAsync(string roleName)
        {
            // Truy vấn danh sách người dùng theo vai trò bằng cách lồng truy vấn
            var users = await _context.Users
                .Where(u => _context.UserRoles
                    .Any(ur => ur.UserId == u.Id && _context.Roles
                        .Any(r => r.Id == ur.RoleId && r.Name == roleName)))
                .ToListAsync();

            return users ?? new List<IdentityUser>();
        }
    }
}
