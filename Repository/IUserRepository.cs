using DisCourse.Models;
using Microsoft.AspNetCore.Identity;

namespace DisCourseW.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync(); // Lấy tất cả user
        Task<IEnumerable<(IdentityUser User, int CourseCount)>> GetUsersWithCourseCountAsync(); // Lấy user kèm số khóa học đã đăng ký
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId);
        IEnumerable<Post> GetPostsByOwner(string userId);
        Task<string> GetUserProfilePictureAsync(string userId);
        Task<IEnumerable<Course>> GetCoursesByUserAsync(string userId);
        Task<List<IdentityUser>> GetUsersByRoleAsync(string roleName);
    }
}
