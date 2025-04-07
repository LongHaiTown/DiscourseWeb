using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using DisCourseW.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace DisCourse.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<PostController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseRepository _userCourseRepository;

        public CourseController(ICourseRepository courseRepository, ILogger<PostController> logger,
            IUserRepository userRepository, IUserCourseRepository userCourseRepository)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _userRepository = userRepository;
            _userCourseRepository = userCourseRepository;
        }

        // 📌 Hiển thị danh sách Course (chia thành 2 phần: đã đăng ký và tất cả khóa học)
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Lấy ID của User đang đăng nhập
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(); // Nếu chưa đăng nhập, từ chối yêu cầu


            // Lấy danh sách các khóa học đã đăng ký
            ViewData["EnrolledCourses"] = (await _userRepository.GetCoursesByUserAsync(userId)) ;

            // Lấy danh sách tất cả các khóa học
            ViewData["AllCourses"] = (await _courseRepository.GetAllAsync()).ToList();

            // Trả về View
            return View();
        }

        // 📌 Hiển thị chi tiết một Course
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var posts = await _courseRepository.GetPostsByCourseIdAsync(id);
            var enrolledUserIds = await _userCourseRepository.GetUsersByCourseAsync(id); // Giả định có phương thức này

            var model = new CourseDetailViewModel
            {
                Course = course,
                Posts = posts,
                EnrolledUserIds = enrolledUserIds
            };

            return View(model);
        }
        // 📌 Hiển thị form tạo Course
        public IActionResult Create()
        {
            return View();
        }

        // 📌 Xử lý tạo Course
        [HttpPost]
        public async Task<IActionResult> Create(Course course, IFormFile? ThumbnailFile)
        {
            // Lấy ID của User đang đăng nhập
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(); // Nếu chưa đăng nhập, từ chối yêu cầu

            if (ThumbnailFile != null)
            {
                course.Thumbnail = await SaveImage(ThumbnailFile);
            }

            // Gán UserID cho bài viết
            course.OwnerID = userId;
            course.CreatedAt = DateTime.UtcNow;
            await _courseRepository.AddAsync(course);
            return RedirectToAction(nameof(Index));
        }

        // Hàm lưu ảnh vào thư mục wwwroot/images
        private async Task<string?> SaveImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null; // Trả về null nếu không có file

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

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

            return "/images/" + uniqueFileName; // Trả về đường dẫn lưu vào database
        }

        // GET: Hiển thị form chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Cập nhật thông tin khóa học
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, IFormFile? ThumbnailFile)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            // Nếu có ảnh mới được upload, lưu ảnh và cập nhật đường dẫn
            if (ThumbnailFile != null)
            {
                var imagePath = await SaveImage(ThumbnailFile);
                course.Thumbnail = imagePath;
            }
            _logger.LogInformation($"OwnerID của khóa học: {course.OwnerID}");
            await _courseRepository.UpdateAsync(course);
            return RedirectToAction(nameof(Index));
        }

        // 📌 Hiển thị form xác nhận xóa
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // 📌 Xử lý xóa Course
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        // 📌 Hiển thị form thêm user vào khóa học (Modal)
        public async Task<IActionResult> AddUserToCourse(int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            //if (course == null) return NotFound();

            var allUsers = await _userRepository.GetUsersByRoleAsync("User");
            var registeredUsers = await _userCourseRepository.GetUsersByCourseAsync(courseId);
            var unregisteredUsers = allUsers.Except(registeredUsers).ToList();

            var model = new UserCourseViewModel
            {
                Course = course,
                RegisteredUsers = (List<Microsoft.AspNetCore.Identity.IdentityUser>)registeredUsers,
                UnregisteredUsers = unregisteredUsers
            };

            return PartialView("_AddUserToCourseModal", model); // Trả về partial view
        }
        // 📌 Xử lý thêm user vào khóa học
        [HttpPost]
        public async Task<IActionResult> AddUserToCourseConfirm(int courseId, List<string> userIds)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);

            if (userIds == null || !userIds.Any())
            {
                TempData["ErrorMessage"] = "Không có học viên nào được chọn để thêm!";
                return RedirectToAction("Details", new { id = courseId });
            }

            var successCount = 0;
            var failCount = 0;

            foreach (var userId in userIds)
            {
                var success = await _userCourseRepository.AddUserToCourseAsync(userId, courseId);
                if (success)
                {
                    successCount++;
                }
                else
                {
                    failCount++;
                }
            }

            if (successCount > 0)
            {
                TempData["SuccessMessage"] = $"Thêm thành công {successCount} học viên vào khóa học!";
            }

            if (failCount > 0)
            {
                TempData["ErrorMessage"] = $"{failCount} học viên đã đăng ký khóa học này trước đó!";
            }

            return RedirectToAction("Details", "Course", new { area = "Teacher", id = courseId });
        }
        //Hàm để xóa người dùng khỏi khóa học
        [HttpPost]
        public async Task<IActionResult> RemoveMultipleUsersFromCourse(int courseId, List<string> userIds)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);

            if (userIds == null || !userIds.Any())
            {
                TempData["ErrorMessage"] = "Không có học viên nào được chọn để xóa!";
                return RedirectToAction("Details", new { id = courseId });
            }

            var successCount = 0;
            var failCount = 0;

            foreach (var userId in userIds)
            {
                var success = await _userCourseRepository.RemoveUserFromCourseAsync(courseId, userId);
                if (success)
                {
                    successCount++;
                }
                else
                {
                    failCount++;
                }
            }

            if (successCount > 0)
            {
                TempData["SuccessMessage"] = $"Xóa thành công {successCount} học viên khỏi khóa học!";
            }

            if (failCount > 0)
            {
                TempData["ErrorMessage"] = $"{failCount} học viên không thể được xóa khỏi khóa học!";
            }

            return RedirectToAction("Details", "Course", new { area = "Teacher", id = courseId });
        }

    }
}
