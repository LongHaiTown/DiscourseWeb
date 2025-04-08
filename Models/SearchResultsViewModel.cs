using DisCourse.Models;

namespace DisCourseW.Models
{
    public class SearchResultsViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public string Query { get; set; }
    }
}
