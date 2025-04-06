using EduquizSuper.Models;

namespace EduquizSuper.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<PaginatedList<Subject>> GetPaginatedAsync(string searchString, string courseId, int pageIndex, int pageSize);
        Task<Subject> GetByIdAsync(string id);
        Task<Subject> GetByIdWithDetailsAsync(string id);
        Task<bool> HasRelatedQuestionsAsync(string id);
        Task AddAsync(Subject subject);
        void Update(Subject subject);
        Task<PaginatedList<Question>> GetQuestionsPaginatedAsync(string subjectId, string searchString, int pageNumber, int pageSize);
        void Delete(Subject subject);
        Task<int> SaveChangesAsync();
        Task<bool> ExistsAsync(string id);
    }
}