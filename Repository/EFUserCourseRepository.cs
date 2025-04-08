using DisCourse.Models;
using DisCourseW.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DisCourseW.Repository
{
    public class EFUserCourseRepository: IUserCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public EFUserCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<(IdentityUser User, int CourseCount)>> GetAllUsersWithCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<(IdentityUser User, int CourseCount)>> GetTopUsersAsync(int top)
        {
            throw new NotImplementedException();
        }

        // Lấy danh sách user đã đăng ký một khóa học
        public async Task<IEnumerable<IdentityUser>> GetUsersByCourseAsync(int courseId)
        {
            return await _context.UserCourses
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.User)
                .ToListAsync();
        }

        public async Task<bool> AddUserToCourseAsync(string userId, int courseId)
        {
            // Kiểm tra xem user đã đăng ký khóa học chưa
            bool exists = await _context.UserCourses
                .AnyAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (exists)
                return false; // User đã đăng ký, không thêm nữa

            var userCourse = new UserCourse
            {
                UserId = userId,
                CourseId = courseId
            };

            _context.UserCourses.Add(userCourse);
            await _context.SaveChangesAsync();
            return true; // Thêm thành công
        }
        public async Task<bool> RemoveUserFromCourseAsync(int courseId, string userId)
        {
            var userCourse = await _context.UserCourses
                .FirstOrDefaultAsync(uc => uc.CourseId == courseId && uc.UserId == userId);

            if (userCourse != null)
            {
                _context.UserCourses.Remove(userCourse);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> IsUserEnrolledInCourseAsync(string userId, int courseId)
        {
            // Kiểm tra xem có bản ghi nào trong bảng UserCourses khớp với userId và courseId
            return await _context.UserCourses
                .AnyAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        }
        public async Task<List<int>> GetEnrolledCourseIdsByUserIdAsync(string userId)
        {
            return await _context.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CourseId)
                .ToListAsync();
        }

    }
}
