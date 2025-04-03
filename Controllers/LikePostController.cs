using DisCourse.Repository;
using DisCourseW.Models;
using DisCourseW.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DisCourseW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikePostController : ControllerBase
    {
        private readonly ILikePostRepository _likePostRepository;
        private readonly IUserRepository _userRepository;

        public LikePostController(ILikePostRepository likePostRepository, IUserRepository userRepository)
        {
            _likePostRepository = likePostRepository;
            _userRepository = userRepository;
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var likePost = new LikePost { PostId = postId, UserId = userId };

            var result = await _likePostRepository.AddLikePostAsync(likePost);
            return CreatedAtAction(nameof(LikePost), new { id = result.Id }, result);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> UnlikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _likePostRepository.RemoveLikePostAsync(postId, userId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{postId}/isLiked")]
        public async Task<IActionResult> IsPostLiked(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isLiked = await _likePostRepository.IsPostLikedByUserAsync(postId, userId);

            return Ok(isLiked);
        }

        [HttpGet("{postId}/likes")]
        public async Task<IActionResult> GetLikes(int postId)
        {
            var likes = await _likePostRepository.GetLikesByPostIdAsync(postId);
            return Ok(likes);
        }
    }
}
