using DisCourse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DisCourseW.Repository
{
    public interface IUserCourseRepository
    {
        Task<IEnumerable<IdentityUser>> GetUsersByCourseAsync(int courseId);
        Task<IEnumerable<(IdentityUser User, int CourseCount)>> GetAllUsersWithCoursesAsync();
        Task<IEnumerable<(IdentityUser User, int CourseCount)>> GetTopUsersAsync(int top);
        Task<bool> AddUserToCourseAsync(string userId, int courseId);
        Task<bool> RemoveUserFromCourseAsync(int courseId, string userId);

    }
}
