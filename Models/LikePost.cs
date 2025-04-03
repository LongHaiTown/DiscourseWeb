namespace DisCourseW.Models
{
    using DisCourse.Models;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LikePost
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // FK tới bảng Users

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        [Required]
        public int PostId { get; set; } // FK tới bảng Courses

        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow; // Thời gian đăng ký

    }
}
