using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using DisCourseW.Repository;
using DisCourseW.Areas.Teacher.Models;
using Microsoft.EntityFrameworkCore;

namespace DisCourseW.Areas.Teacher.Controllers
{

    [Area("Teacher")]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseRepository _userCourseRepository;

        public CourseController(ICourseRepository courseRepository,
            IUserRepository userRepository, IUserCourseRepository userCourseRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _userCourseRepository = userCourseRepository;
        }

        // 📌 Hiển thị danh sách Course
        public async Task<IActionResult> Index()
        {
            var courses = await _userRepository.GetCoursesByUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = _userRepository.GetPostsByOwner(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var viewModel = new CoursePostView
            {
                Courses = courses,
                Posts = posts
            };
            return View(viewModel);
        }

        // 📌 Hiển thị chi tiết một Course
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            var posts = await _courseRepository.GetPostsByCourseIdAsync(id);
            var viewModel = new CourseDetailViewModel
            {
                Course = course,
                Posts = posts
            };

            return View(viewModel);
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

            // Lấy URL của trang trước đó từ Header "Referer"
            var previousUrl = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(previousUrl))
            {
                return Redirect(previousUrl); // Quay lại trang trước đó
            }

            // Nếu không có URL trước đó, quay về Index
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
        //// 📌 Hiển thị form thêm user vào khóa học (Modal)
        //public async Task<IActionResult> AddUserToCourse(int courseId)
        //{
        //    var course = await _courseRepository.GetByIdAsync(courseId);
        //    //if (course == null) return NotFound();

        //    var allUsers = await _userRepository.GetAllUsersAsync();
        //    var registeredUsers = await _userCourseRepository.GetUsersByCourseAsync(courseId);
        //    var unregisteredUsers = allUsers.Except(registeredUsers).ToList();

        //    var model = new UserCourseViewModel
        //    {
        //        Course =  course,
        //        RegisteredUsers = (List<Microsoft.AspNetCore.Identity.IdentityUser>)registeredUsers,
        //        UnregisteredUsers = unregisteredUsers
        //    };

        //    return PartialView("_AddUserToCourseModal", model); // Trả về partial view
        //}
        //// 📌 Xử lý thêm user vào khóa học
        //[HttpPost]
        //public async Task<IActionResult> AddUserToCourseConfirm(int courseId, string userId)
        //{
        //    var course = await _courseRepository.GetByIdAsync(courseId);

        //    bool success = await _userCourseRepository.AddUserToCourseAsync(userId, courseId);

        //    if (success)
        //    {
        //        TempData["SuccessMessage"] = "Thêm user vào khóa học thành công!";
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "User đã đăng ký khóa học này!";
        //    }

        //    return RedirectToAction("Details", new { id = courseId });
        //}
        // 📌 Hiển thị danh sách User đã đăng ký trong khóa học (Gọi Modal)
        //public async Task<IActionResult> ViewRegisteredUsers(int courseId)
        //{
        //    var course = await _courseRepository.GetByIdAsync(courseId);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    var registeredUsers = await _userCourseRepository.GetUsersByCourseAsync(courseId);

        //    var model = new UserCourseViewModel
        //    {
        //        Course = course,
        //        Users = registeredUsers.ToList()
        //    };

        //    return PartialView("_ViewRegisteredUsersModal", model);
        //}
        // Hàm để xóa người dùng khỏi khóa học
        //[HttpPost]
        //public async Task<IActionResult> RemoveUserFromCourse(int courseId, string userId)
        //{
        //    var course = await _courseRepository.GetByIdAsync(courseId);
        //    var result = await _userCourseRepository.RemoveUserFromCourseAsync(courseId, userId);

        //    if (result)
        //    {
        //        TempData["SuccessMessage"] = "Người dùng đã được xóa khỏi khóa học thành công.";
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa người dùng khỏi khóa học.";
        //    }

        //    var registeredUsers = await _userCourseRepository.GetUsersByCourseAsync(courseId);

        //    var model = new UserCourseViewModel
        //    { 
        //        Course = course,
        //        Users = registeredUsers.ToList()
        //    };

        //    return RedirectToAction("Details" , new {id = courseId });
        //}
    }
}
