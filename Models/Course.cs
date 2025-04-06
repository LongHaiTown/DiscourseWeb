using EduquizSuper.Models;
using System.ComponentModel.DataAnnotations;

namespace EduquizSuper.Models
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        public string Description { get; set; }

        // Navigation properties
        public List<User> Users { get; set; } = new List<User>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
