using DisCourse.Models;

namespace DisCourseW.Models
{
    public class AccountIndexViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();
        public List<Post> UserPosts { get; set; } = new List<Post>();
    }
}
