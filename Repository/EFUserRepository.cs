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

        public Task<IEnumerable<(IdentityUser User, int CourseCount)>> GetUsersWithCourseCountAsync()
        {
            throw new NotImplementedException();
        }


    }
}
