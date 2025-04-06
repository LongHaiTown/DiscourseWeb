using EduquizSuper.Models;
using System.ComponentModel.DataAnnotations;

namespace EduquizSuper.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [Display(Name = "Khóa học")]
        public string CourseName { get; set; }

        public string CourseId { get; set; }

        [Display(Name = "Vai trò")]
        public List<string> Roles { get; set; }

        [Display(Name = "Kết quả thi gần đây")]
        public IEnumerable<ExamResult> ExamResults { get; set; }
    }
}
