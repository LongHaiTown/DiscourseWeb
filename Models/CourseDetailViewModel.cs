using DisCourse.Models;
using Microsoft.AspNetCore.Identity;

public class CourseDetailViewModel
{
    public Course Course { get; set; }
    public IEnumerable<Post> Posts { get; set; }
    public IEnumerable<IdentityUser> EnrolledUserIds { get; set; } // Danh sách ID của học viên đã đăng ký

}