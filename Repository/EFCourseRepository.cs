using EduquizSuper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EduquizSuper.Data
{
    public class EFCourseRepository : ICourseRepository
    {
        private readonly EduquizContextDb _context;

        public EFCourseRepository(EduquizContextDb context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }
        public async Task<PaginatedList<Course>> GetCoursesAsync(string searchString, int pageNumber, int pageSize)
        {
            var courses = _context.Courses.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.CourseName.Contains(searchString));
            }
            return await PaginatedList<Course>.CreateAsync(courses.AsNoTracking(), pageNumber, pageSize);
        }

        public async Task<Course> GetCourseByIdAsync(string id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Courses.AnyAsync(e => e.CourseId == id);
        }
        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(string id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CourseExistsAsync(string id)
        {
            return await _context.Courses.AnyAsync(e => e.CourseId == id);
        }
    }
}