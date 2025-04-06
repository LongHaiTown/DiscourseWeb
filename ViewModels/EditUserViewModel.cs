using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EduquizSuper.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Khóa học")]
        public string CourseId { get; set; }

        [Display(Name = "Ảnh đại diện hiện tại")]
        public string CurrentAvatar { get; set; }

        [Display(Name = "Tải lên ảnh đại diện mới")]
        public IFormFile Avatar { get; set; }

        [Display(Name = "Vai trò người dùng")]
        public List<string> UserRoles { get; set; }

        [Display(Name = "Các vai trò")]
        public List<SelectListItem> AllRoles { get; set; }

        [Display(Name = "Chọn vai trò")]
        public List<string> SelectedRoles { get; set; }
    }
}
