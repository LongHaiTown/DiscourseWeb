using EduquizSuper.Models;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace EduquizSuper.Data
{
    public interface ICourseRepository
    {
        Task<PaginatedList<Course>> GetCoursesAsync(string searchString, int pageNumber, int pageSize);
        Task<Course> GetCourseByIdAsync(string id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(string id);
        Task<bool> CourseExistsAsync(string id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<bool> ExistsAsync(string id);
    }
}