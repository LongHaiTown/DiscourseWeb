using DisCourse.Models;
using DisCourseW.Models;
using Microsoft.EntityFrameworkCore;

namespace DisCourse.Repository
{
    public class EFLikePostRepository : ILikePostRepository
    {
        private readonly ApplicationDbContext _context;

        public EFLikePostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LikePost> AddLikePostAsync(LikePost likePost)
        {
            await _context.LikePosts.AddAsync(likePost);
            await _context.SaveChangesAsync();
            return likePost;
        }

        public async Task<bool> RemoveLikePostAsync(int postId, string userId)
        {
            var likePost = await _context.LikePosts
                .FirstOrDefaultAsync(lp => lp.PostId == postId && lp.UserId == userId);

            if (likePost != null)
            {
                _context.LikePosts.Remove(likePost);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> IsPostLikedByUserAsync(int postId, string userId)
        {
            return await _context.LikePosts
                .AnyAsync(lp => lp.PostId == postId && lp.UserId == userId);
        }

        public async Task<IEnumerable<LikePost>> GetLikesByPostIdAsync(int postId)
        {
            return await _context.LikePosts
                .Where(lp => lp.PostId == postId)
                .ToListAsync();
        }
    }
}
