using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Thêm này cho IFormFile

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Security.Claims;
using DisCourseW.Models;
using Microsoft.AspNetCore.Authorization;
using DisCourseW.Repository; // Thêm namespace này để lấy UserID


namespace DisCourse.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserCourseRepository _userCourseRepository; // Thêm repository này
        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepository postRepository, ICourseRepository courseRepository,ICommentRepository commentRepository, ILogger<PostController> logger, IUserCourseRepository userCourseRepository)
        {
            _postRepository = postRepository;
            _courseRepository = courseRepository;
            _userCourseRepository = userCourseRepository; // Gán vào field
            _commentRepository = commentRepository;
            _logger = logger;
        }
        // Hàm kiểm tra quyền truy cập bài viết
        private async Task<bool> CanUserAccessPost(int postId)
        {
            // Lấy ID người dùng hiện tại
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !User.Identity.IsAuthenticated)
            {
                return false; // Chưa đăng nhập thì không có quyền
            }

            // Lấy thông tin bài viết
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                return false; // Bài viết không tồn tại
            }

            // Lấy thông tin khóa học chứa bài viết
            var course = post.Course;
            if (course == null)
            {
                return false; // Khóa học không tồn tại
            }

            // Điều kiện 1: Người dùng đã đăng ký trong UserCourse
            var isEnrolled = await _userCourseRepository.IsUserEnrolledInCourseAsync(userId, course.Id);
            if (isEnrolled)
            {
                return true; // Đã đăng ký thì có quyền truy cập
            }

            // Điều kiện 2: Bài viết có Author không phải là Owner của khóa học
            if (post.AuthorId != course.OwnerID)
            {
                return true; // Bài viết cộng đồng thì ai cũng xem được
            }

            // Không thỏa mãn điều kiện nào
            return false;
        }

        // Hiển thị danh sách bài viết
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllAsync();
            return View(posts);
        }

        // Hiển thị chi tiết bài viết
        [Authorize]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var comments = await _commentRepository.GetCommentsByPostIdAsync(id);
            var canAccess = await CanUserAccessPost(id); // Lấy giá trị bool

            // Truyền dữ liệu vào ViewBag
            ViewBag.Comments = comments;
            ViewBag.CanAccess = canAccess;

            return View(post);
        }
        // 📌 Hiển thị form tạo bài viết
        [Authorize]
        public async Task<IActionResult> Create()
        {

            ViewBag.Courses = await _courseRepository.GetAllAsync(); // Gửi danh sách Course xuống View
            return View();
        }


        // 📌 Xử lý tạo bài viết
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, IFormFile? ThumbnailFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            if (ThumbnailFile != null)
            {
                post.Thumbnail = await SaveImage(ThumbnailFile);
            }

            post.AuthorId = userId;

            _logger.LogInformation($"CourseId: {post.CourseId}");

            _logger.LogInformation("Bài viết mới được tạo!");
            await _postRepository.AddAsync(post);

            // Quay lại trang trước đó
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // 📌 Hiển thị form chỉnh sửa bài viết
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return NotFound();

            ViewBag.Courses = await _courseRepository.GetAllAsync(); // Gửi danh sách Course xuống View
            return View(post);
        }

        // 📌 Xử lý chỉnh sửa bài viết
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {

            //if (!ModelState.IsValid)
            //{
            //    ViewBag.Courses = await _courseRepository.GetAllAsync();
            //    return View(post);
            //}

            post.AuthorId = post.AuthorId;
            await _postRepository.UpdateAsync(post);
            return RedirectToAction(nameof(Index));
        }

        // Hiển thị xác nhận xóa bài viết
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // Xử lý xóa bài viết
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postRepository.DeleteAsync(id);
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
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload) // CKEditor sends the file with parameter name 'upload'
        {
            try
            {
                if (upload != null && upload.Length > 0)
                {
                    // Tạo thư mục nếu chưa có
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Tạo tên file ngẫu nhiên để tránh trùng
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                    var filePath = Path.Combine(uploadFolder, fileName);

                    // Lưu file vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await upload.CopyToAsync(stream);
                    }

                    // Tạo URL đầy đủ
                    var imageUrl = $"/images/{fileName}";

                    // Return response in CKEditor 5 expected format
                    return Json(new
                    {
                        uploaded = 1,
                        fileName = fileName,
                        url = imageUrl
                    });
                }
                else
                {
                    return Json(new
                    {
                        uploaded = 0,
                        error = new { message = "Không có tệp nào được tải lên!" }
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    uploaded = 0,
                    error = new { message = $"Lỗi tải ảnh: {ex.Message}" }
                });
            }
        }
    }
}
