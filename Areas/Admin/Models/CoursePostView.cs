using DisCourse.Models;

namespace DisCourseW.Areas.Admin.Models
{
    public class CoursePostView
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }

}
