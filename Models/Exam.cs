using EduquizSuper.Models;
using System.ComponentModel.DataAnnotations;

namespace EduquizSuper.Models
{
    public class Exam
    {
        [Required]
        public string ExamId { get; set; }
        [Required]
        public string ExamName { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int EasyQuestions { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MediumQuestions { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int HardQuestions { get; set; }

        [Required]
        public int NumberOfQuestions { get; set; } // Thuộc tính thông thường

        [Required]
        public double ScorePerQuestion { get; set; }

        public double TotalScore { get; set; } // Có thể giữ thông thường hoặc computed

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Thời gian làm bài phải lớn hơn 0")]
        public int Duration { get; set; } // Thời gian làm bài tính bằng phút

        [Required]
        public string SubjectId { get; set; }
        public string? ManagerId { get; set; }

        public Subject? Subject { get; set; }
        public User? Manager { get; set; }
        public List<ExamDetail>? ExamDetails { get; set; }

    }
}
