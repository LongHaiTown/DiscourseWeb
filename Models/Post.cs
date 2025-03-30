using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisCourse.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } // Tiêu đề bài viết

        [Required]
        public string Content { get; set; } // Nội dung (HTML từ CKEditor)

        public string? Thumbnail { get; set; } // Ảnh đại diện (nếu có)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Thời gian tạo

        public string? Author { get; set; } // Người viết bài

        // Khóa ngoại trỏ đến Course
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

    }
}
