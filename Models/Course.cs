using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DisCourse.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } // Tên khóa học

        [Required, MaxLength(500)]
        public string Description { get; set; } // Mô tả khóa học

        public string? Thumbnail { get; set; } // Ảnh đại diện khóa học

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Ngày tạo

        // Khóa học có nhiều bài viết (1-N)
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
