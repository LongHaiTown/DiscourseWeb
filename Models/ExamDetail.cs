using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EduquizSuper.Models;

namespace EduquizSuper.Models
{
    public class ExamDetail
    {
        [Key, Column(Order = 0)]
        public string ExamId { get; set; }

        [Key, Column(Order = 1)]
        public string QuestionId { get; set; }

        public bool IsCorrect { get; set; }

        // Navigation properties
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}