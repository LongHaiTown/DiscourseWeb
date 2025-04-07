using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DisCourseW.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserProfilePicture
    {
        [Key]
        public int Id { get; set; }
        // Khóa ngoại liên kết đến ApplicationUser
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        // Đường dẫn đến ảnh đại diện
        public string ProfilePictureUrl { get; set; }
    }
}
