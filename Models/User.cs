using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace EduquizSuper.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Avatar { get; set; }
        public string? CourseId { get; set; }

        // Navigation properties
        public Course Course { get; set; }
        public List<Exam> ManagedExams { get; set; } = new List<Exam>();
        public List<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }

}
