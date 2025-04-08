using DisCourse.Controllers;
using DisCourse.Repository;
using DisCourseW.Models;
using DisCourseW.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DisCourseW.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPostRepository _postRepository;

        private readonly ILogger<PostController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IUserProfilePictureRepository _userProfilePictureRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(ICourseRepository courseRepository, IPostRepository postRepository, ILogger<PostController> logger, IUserRepository userRepository,IUserCourseRepository userCourseRepository,IUserProfilePictureRepository userProfilePictureRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _courseRepository = courseRepository;
            _postRepository = postRepository;
            _logger = logger;
            _userRepository = userRepository;
            _userCourseRepository = userCourseRepository;
            _userProfilePictureRepository = userProfilePictureRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Hàm lưu ảnh vào thư mục wwwroot/images
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID người dùng hiện tại
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng nếu chưa đăng nhập
            }

            // Lấy thông tin người dùng
            var user = await _userRepository.GetUserByIdAsync(userId);
            var userProfilePicture = await _userProfilePictureRepository.GetProfilePictureByUserIdAsync(userId);

            // Lấy danh sách khóa học đã đăng ký từ UserCourse
            var enrolledCourseIds = await _userCourseRepository.GetEnrolledCourseIdsByUserIdAsync(userId);
            var enrolledCourses = await _courseRepository.GetCoursesByIdsAsync(enrolledCourseIds);

            // Lấy danh sách bài viết của người dùng
            var userPosts = await _postRepository.GetPostsByAuthorIdAsync(userId);

            // Tạo ViewModel
            var model = new AccountIndexViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePictureUrl = userProfilePicture?.ProfilePictureUrl,
                EnrolledCourses = enrolledCourses,
                UserPosts = userPosts
            };

            return View(model); // Trả về view với model
        }

        //public async Task<IActionResult> Profile()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID người dùng hiện tại
        //    var user = await _userRepository.GetUserByIdAsync(userId); // Lấy thông tin người dùng từ repository
        //    var userProfilePicture = await _userProfilePictureRepository.GetProfilePictureByUserIdAsync(userId); // Lấy ảnh đại diện

        //    var model = new UserProfileViewModel
        //    {
        //        UserId = user.Id,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        ProfilePictureUrl = userProfilePicture?.ProfilePictureUrl // Lấy đường dẫn ảnh đại diện
        //    };

        //    return View(model); // Trả về view với model chứa thông tin người dùng
        //}
    
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

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        // Action POST để xử lý đăng ký và assign role "User"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(
            [Bind("Email,Password,ConfirmPassword")]
            RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = new IdentityUser // Thay ApplicationUser bằng IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Kiểm tra và tạo role "User" nếu chưa tồn tại
                    const string roleName = "User";
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        var role = new IdentityRole
                        {
                            Id = "83b2ac78-3263-44e2-a953-6db2b76ca0ef",
                            Name = roleName,
                            NormalizedName = roleName.ToUpper()
                        };
                        await _roleManager.CreateAsync(role);
                    }

                    // Assign role "User" cho user mới
                    await _userManager.AddToRoleAsync(user, roleName);

                    // Đăng nhập ngay sau khi đăng ký
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi tạo tài khoản. Vui lòng thử lại sau.");
            }

            return View(model);
        }
    }


}
