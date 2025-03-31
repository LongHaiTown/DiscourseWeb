using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DisCourse.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        // Xem tất cả bình luận của một bài viết
        public async Task<IActionResult> Index(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return NotFound();

            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            ViewBag.Post = post; // Để hiển thị thông tin bài viết
            return View(comments);
        }
        [HttpPost]
        [Authorize] // Đảm bảo người dùng đã đăng nhập

        public async Task<IActionResult> Create(Comment comment)
        {
            // Lấy ID của User đang đăng nhập
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized(); // Nếu chưa đăng nhập, từ chối yêu cầu

            // Gán UserID cho bài viết
            comment.AuthorId = userId;

            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                TempData["Error"] = "Nội dung bình luận không được để trống!";
                return RedirectToAction("Details", "Post", new { id = comment.PostId });
            }

            var post = await _postRepository.GetByIdAsync(comment.PostId);
            if (post == null)
            {
                return NotFound(); // Trả về 404 nếu bài viết không tồn tại
            }

            comment.CreatedAt = DateTime.UtcNow; // Thêm timestamp
            await _commentRepository.AddCommentAsync(comment);

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

    }
}
