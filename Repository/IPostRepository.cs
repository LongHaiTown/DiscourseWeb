using DisCourse.Models;

namespace DisCourse.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task AddAsync(Post product);
        Task UpdateAsync(Post product);
        Task DeleteAsync(int id);
    }
}
