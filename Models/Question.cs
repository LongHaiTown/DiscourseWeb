using EduquizSuper.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduquizSuper.Models
{
    public class Question
    {
        [Key]
        public string QuestionId { get; set; }

        [Required]
        public string QuestionContent { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string AnswersJson { get; set; }

        public string SubjectId { get; set; }

        public string Difficulty { get; set; }

        // Navigation properties
        public Subject? Subject { get; set; }
        public List<ExamDetail> ExamDetails { get; set; } = new List<ExamDetail>();
    }
}
