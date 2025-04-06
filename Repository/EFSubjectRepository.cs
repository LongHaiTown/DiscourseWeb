using EduquizSuper.Data;
using EduquizSuper.Models;
using Microsoft.EntityFrameworkCore;

namespace EduquizSuper.Repositories
{
    public class EFSubjectRepository : ISubjectRepository
    {
        private readonly EduquizContextDb _context;

        public EFSubjectRepository(EduquizContextDb context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        public async Task<PaginatedList<Subject>> GetPaginatedAsync(string searchString, string courseId, int pageIndex, int pageSize)
        {
            var query = _context.Subjects.Include(s => s.Course).AsQueryable();

            if (!string.IsNullOrEmpty(courseId))
            {
                query = query.Where(s => s.CourseId == courseId);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.SubjectName.Contains(searchString));
            }

            return await PaginatedList<Subject>.CreateAsync(query.OrderBy(s => s.CourseId), pageIndex, pageSize);
        }

        public async Task<Subject> GetByIdAsync(string id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Subject> GetByIdWithDetailsAsync(string id)
        {
            return await _context.Subjects
                .Include(s => s.Course)
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.SubjectId == id);
        }

        public async Task<bool> HasRelatedQuestionsAsync(string id)
        {
            return await _context.Questions.AnyAsync(q => q.SubjectId == id);
        }

        public async Task<PaginatedList<Question>> GetQuestionsPaginatedAsync(string subjectId, string searchString, int pageNumber, int pageSize)
        {
            var questions = _context.Questions
                .Where(q => q.SubjectId == subjectId);

            if (!string.IsNullOrEmpty(searchString))
            {
                questions = questions.Where(q => q.QuestionContent.Contains(searchString));
            }

            questions = questions.OrderBy(q => q.QuestionId); // Sắp xếp để phân trang ổn định

            return await PaginatedList<Question>.CreateAsync(questions.AsNoTracking(), pageNumber, pageSize);
        }

        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
        }

        public void Update(Subject subject)
        {
            _context.Subjects.Update(subject);
        }

        public void Delete(Subject subject)
        {
            _context.Subjects.Remove(subject);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Subjects.AnyAsync(s => s.SubjectId == id);
        }
    }
}