using EduquizSuper.Models;

namespace EduquizSuper.Repository
{
    public interface IExamRepository
    {
        Task<PaginatedList<Exam>> GetPaginatedAsync(int pageIndex, int pageSize);
        Task<Exam> GetByIdAsync(string id);
        Task<Exam> GetByIdWithDetailsAsync(string id);
        Task AddAsync(Exam exam);
        void Update(Exam exam);
        void Delete(Exam exam);
        Task<int> SaveChangesAsync();
        Task<bool> ExistsAsync(string id);
        Task<List<Question>> GetQuestionsByDifficultyAsync(string examId, string difficulty);
    }
}
