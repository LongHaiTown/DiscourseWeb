using DisCourse.Controllers;
using DisCourse.Repository;
using DisCourseW.Models;
using DisCourseW.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DisCourseW.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<PostController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IUserProfilePictureRepository _userProfilePictureRepository;

        public AccountController(ICourseRepository courseRepository, ILogger<PostController> logger, IUserRepository userRepository,IUserCourseRepository userCourseRepository,IUserProfilePictureRepository userProfilePictureRepository)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _userRepository = userRepository;
            _userCourseRepository = userCourseRepository;
            _userProfilePictureRepository = userProfilePictureRepository;

        }
        
        // Hàm lưu ảnh vào thư mục wwwroot/images
        public IActionResult Index()
        {
            return RedirectToAction("Profile"); // Chuyển hướng đến trang hồ sơ
        }
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID người dùng hiện tại
            var user = await _userRepository.GetUserByIdAsync(userId); // Lấy thông tin người dùng từ repository
            var userProfilePicture = await _userProfilePictureRepository.GetProfilePictureByUserIdAsync(userId); // Lấy ảnh đại diện

            var model = new UserProfileViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePictureUrl = userProfilePicture?.ProfilePictureUrl // Lấy đường dẫn ảnh đại diện
            };

            return View(model); // Trả về view với model chứa thông tin người dùng
        }
    
        private async Task<string?> SaveImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null; // Trả về null nếu không có file

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profilepic");

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Tạo tên file duy nhất
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/profilepic/" + uniqueFileName; // Trả về đường dẫn lưu vào database
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(IFormFile imageFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID người dùng hiện tại

            // Lưu ảnh vào thư mục và lấy đường dẫn
            var imageUrl = await SaveImage(imageFile);
            if (imageUrl != null)
            {
                // Kiểm tra xem người dùng đã có ảnh đại diện chưa
                var userProfilePicture = await _userProfilePictureRepository.GetProfilePictureByUserIdAsync(userId);

                if (userProfilePicture == null)
                {
                    // Tạo mới nếu chưa có ảnh đại diện
                    userProfilePicture = new UserProfilePicture
                    {
                        UserId = userId,
                        ProfilePictureUrl = imageUrl
                    };
                    await _userProfilePictureRepository.AddProfilePictureAsync(userProfilePicture);
                }
                else
                {
                    // Cập nhật nếu đã có ảnh đại diện
                    userProfilePicture.ProfilePictureUrl = imageUrl;
                    await _userProfilePictureRepository.UpdateProfilePictureAsync(userProfilePicture);
                }

                return RedirectToAction("Profile"); // Chuyển hướng đến trang hồ sơ
            }

            // Nếu không có ảnh, có thể trả về một thông báo lỗi hoặc làm gì đó khác
            ModelState.AddModelError("", "Không thể lưu ảnh đại diện.");
            return View("Index"); // Trả về view hiện tại với thông báo lỗi
        }
    

    }
}
