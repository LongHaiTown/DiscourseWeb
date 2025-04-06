using EduquizSuper.Models;

namespace EduquizSuper.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetBySubjectIdAsync(string subjectId);
        Task<List<Question>> GetByIdsAsync(List<string> questionIds);
    }
}