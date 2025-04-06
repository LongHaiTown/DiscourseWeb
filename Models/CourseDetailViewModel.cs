using DisCourse.Models;

public class CourseDetailViewModel
{
    public Course Course { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}
