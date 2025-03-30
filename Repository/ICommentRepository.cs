using DisCourse.Models;

namespace DisCourse.Repository
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task AddCommentAsync(Comment comment);
        Task UpdateUserIdAsync(int commentId, string userId);

    }
}
