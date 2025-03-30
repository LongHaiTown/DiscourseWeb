using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisCourse.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; } // Khóa ngoại đến Post

        [ForeignKey("PostId")]
        public Post Post { get; set; } // Điều hướng đến bài viết

        //[ForeignKey("User")]
        //public string? UserId { get; set; } // Cho phép null
        //public User? User { get; set; } // Điều hướng đến User

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
