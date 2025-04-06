using EduquizSuper.Data;
using EduquizSuper.Models;
using Microsoft.EntityFrameworkCore;

namespace EduquizSuper.Repositories
{
    public class EFQuestionRepository : IQuestionRepository
    {
        private readonly EduquizContextDb _context;

        public EFQuestionRepository(EduquizContextDb context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetBySubjectIdAsync(string subjectId)
        {
            return await _context.Questions
                .Where(q => q.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<List<Question>> GetByIdsAsync(List<string> questionIds)
        {
            return await _context.Questions
                .Where(q => questionIds.Contains(q.QuestionId))
                .ToListAsync();
        }
    }
}