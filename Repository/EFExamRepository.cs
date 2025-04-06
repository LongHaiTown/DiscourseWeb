using EduquizSuper.Data;
using EduquizSuper.Models;
using Microsoft.EntityFrameworkCore;

namespace EduquizSuper.Repository
{
    public class EFExamRepository : IExamRepository
    {
        private readonly EduquizContextDb _context;

        public EFExamRepository(EduquizContextDb context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Exam>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            var exams = _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.Manager)
                .OrderByDescending(e => e.StartTime);
            return await PaginatedList<Exam>.CreateAsync(exams.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<Exam> GetByIdAsync(string id)
        {
            return await _context.Exams.FindAsync(id);
        }

        public async Task<Exam> GetByIdWithDetailsAsync(string id)
        {
            return await _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.Manager)
                .Include(e => e.ExamDetails)
                    .ThenInclude(ed => ed.Question)
                .FirstOrDefaultAsync(e => e.ExamId == id);
        }

        public async Task AddAsync(Exam exam)
        {
            await _context.Exams.AddAsync(exam);
        }

        public void Update(Exam exam)
        {
            _context.Exams.Update(exam);
        }

        public void Delete(Exam exam)
        {
            _context.Exams.Remove(exam);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Exams.AnyAsync(e => e.ExamId == id);
        }
        public async Task<List<Question>> GetQuestionsByDifficultyAsync(string examId, string difficulty)
        {
            return await _context.ExamDetails
                .Where(ed => ed.ExamId == examId && ed.Question.Difficulty == difficulty)
                .Select(ed => ed.Question)
                .ToListAsync();
        }
    }
}