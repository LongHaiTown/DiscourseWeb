using DisCourse.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisCourse.Repository
{
    public class EFPostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public EFPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả bài viết
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.Author) // Load User liên quan
                .Include(p => p.Course) // Load Course liên quan
                .ToListAsync();
        }

        // Lấy bài viết theo ID
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Author) // Load User liên quan
                .Include(p => p.Course) // Load Course liên quan
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        // Thêm bài viết mới
        public async Task AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        // Cập nhật bài viết
        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        // Xóa bài viết
        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
