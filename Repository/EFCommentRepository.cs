using DisCourse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DisCourse.Repository
{
    public class EFCommentRepository: ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        // Thêm bình luận mới (cho phép UserId null)
        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public Task UpdateUserIdAsync(int commentId, string userId)
        {
            throw new NotImplementedException();
        }

        // Cập nhật UserId sau này (nếu cần)
        //public async Task UpdateUserIdAsync(int commentId, string userId)
        //{
        //    var comment = await _context.Comments.FindAsync(commentId);
        //    if (comment != null)
        //    {
        //        comment.UserId = userId;
        //        await _context.SaveChangesAsync();
        //    }
        //}

    }
}
