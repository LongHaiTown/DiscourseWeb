namespace DisCourseW.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DisCourse.Models;
    using Microsoft.AspNetCore.Identity;
    public class UserCourse
    {
        [Key]
        public int Id { get; set; } // Khóa chính của bảng liên kết

        [Required]
        public string UserId { get; set; } // FK tới bảng Users

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        [Required]
        public int CourseId { get; set; } // FK tới bảng Courses

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow; // Thời gian đăng ký
    }
}
