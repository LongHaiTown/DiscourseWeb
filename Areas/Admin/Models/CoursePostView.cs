using DisCourse.Models;
using Microsoft.AspNetCore.Identity;

namespace DisCourseW.Areas.Admin.Models
{
    public class CoursePostView
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; } // Thêm danh sách người dùng
    }

}
