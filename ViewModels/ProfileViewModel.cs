using System.ComponentModel.DataAnnotations;

namespace EduquizSuper.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Ảnh đại diện mới")]
        public IFormFile Avatar { get; set; }

        [Display(Name = "Ảnh đại diện hiện tại")]
        public string CurrentAvatar { get; set; }

        //[Display(Name = "Khóa học")]
        public string? CourseId { get; set; }
    }
}
