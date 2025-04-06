using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Thêm này cho IFormFile

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

        //[HttpPost("{postId}")]
        //public async Task<IActionResult> LikePost(int postId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var likePost = new LikePost { PostId = postId, UserId = userId };

        //    var result = await _likePostRepository.AddLikePostAsync(likePost);
        //    return CreatedAtAction(nameof(LikePost), new { id = result.Id }, result);
        //}


        ////API for like
        //[HttpDelete("{postId}")]
        //public async Task<IActionResult> UnlikePost(int postId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var result = await _likePostRepository.RemoveLikePostAsync(postId, userId);

        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        //[HttpGet("{postId}/isLiked")]
        //public async Task<IActionResult> IsPostLiked(int postId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var isLiked = await _likePostRepository.IsPostLikedByUserAsync(postId, userId);

        //    return Ok(isLiked);
        //}

        //[HttpGet("{postId}/likes")]
        //public async Task<IActionResult> GetLikes(int postId)
        //{
        //    var likes = await _likePostRepository.GetLikesByPostIdAsync(postId);
        //    return Ok(likes);
        //}
    }
}
