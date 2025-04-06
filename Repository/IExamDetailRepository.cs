using EduquizSuper.Models;

namespace EduquizSuper.Repository
{
    public interface IExamDetailRepository
    {
        Task<List<ExamDetail>> GetByExamIdAsync(string examId);
        Task AddRangeAsync(IEnumerable<ExamDetail> examDetails);
        Task RemoveRangeAsync(IEnumerable<ExamDetail> examDetails);
        Task<int> SaveChangesAsync();
    }
}