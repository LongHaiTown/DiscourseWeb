using DisCourseW.Models;

namespace DisCourse.Repository
{
    public interface ILikePostRepository
    {
        Task<LikePost> AddLikePostAsync(LikePost likePost);
        Task<bool> RemoveLikePostAsync(int postId, string userId);
        Task<bool> IsPostLikedByUserAsync(int postId, string userId);
        Task<IEnumerable<LikePost>> GetLikesByPostIdAsync(int postId);
    }
}
