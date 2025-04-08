using DisCourse.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisCourse.Repository
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
        IEnumerable<Course> GetTopCourses(int count);

        // 🆕 Hàm mới để lấy tất cả bài viết của một khóa học
        Task<IEnumerable<Post>> GetPostsByCourseIdAsync(int courseId);

        Task<List<Course>> GetCoursesByIdsAsync(List<int> courseIds);
    }
}
