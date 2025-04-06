using EduquizSuper.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduquizSuper.Models
{
    public class ExamResult
    {
        [Key, Column(Order = 0)]
        public string ExamId { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public int CorrectCount { get; set; } 
        public int IncorrectCount { get; set; }
        public int UnansweredCount { get; set; }
        public double Score { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime SubmitTime { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string UserAnswersJson { get; set; }

        // Navigation properties
        public Exam Exam { get; set; }
        public User User { get; set; }

    }
}
