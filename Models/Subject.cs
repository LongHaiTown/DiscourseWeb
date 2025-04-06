using System.ComponentModel.DataAnnotations;

namespace EduquizSuper.Models
{
    public class Subject
    {
        [Key]
        public string SubjectId { get; set; }

        [Required]
        public string SubjectName { get; set; }

        public string? CourseId { get; set; }

        // Navigation properties
        public Course? Course { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
