using DisCourse.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisCourse.Repository
{
    public class EFCourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(p => p.Owner) // Load User
                .ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(p => p.Owner) // Load User
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Post>> GetPostsByCourseIdAsync(int courseId)
        {
            return await _context.Posts
                .Where(p => p.CourseId == courseId)
                .Include(p => p.Author) // Load thông tin Author của bài viết
                .ToListAsync();
        }
        // Phương thức để lấy một số lượng khóa học cụ thể
        public IEnumerable<Course> GetTopCourses(int count)
        {
            return _context.Courses
                           .Take(count) // Lấy số lượng khóa học được chỉ định
                           .ToList();  // Chuyển đổi thành danh sách
        }
        public async Task<List<Course>> GetCoursesByIdsAsync(List<int> courseIds)
        {
            return await _context.Courses
                .Where(c => courseIds.Contains(c.Id))
                .Include(c => c.Owner) // Include Owner để lấy UserName
                .ToListAsync();
        }

    }
}
