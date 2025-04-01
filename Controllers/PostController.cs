using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Add this for IFormFile

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Security.Claims;
using DisCourseW.Models;
using Microsoft.AspNetCore.Authorization; // Thêm namespace này để lấy UserID


namespace DisCourse.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICommentRepository _commentRepository;

        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepository postRepository, ICourseRepository courseRepository,ICommentRepository commentRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _courseRepository = courseRepository;
            _commentRepository = commentRepository;
            _logger = logger;
        }

        // Hiển thị danh sách bài viết
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllAsync();
            return View(posts);
        }

        // Hiển thị chi tiết bài viết
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var comments = await _commentRepository.GetCommentsByPostIdAsync(id);
            ViewBag.Comments = comments;

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
        public async Task<IActionResult> Create(Post post)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

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
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload) // CKEditor sends the file with parameter name 'upload'
        {
            try
            {
                if (upload != null && upload.Length > 0)
                {
                    // Tạo thư mục nếu chưa có
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
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
                    var imageUrl = $"/uploads/{fileName}"; // Use relative URL

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
