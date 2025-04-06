using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EduquizSuper.Models;

namespace EduquizSuper.Data
{
    public class EduquizContextDb : IdentityDbContext<User>
    {
        public EduquizContextDb(DbContextOptions<EduquizContextDb> options)
            : base(options)
        {
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamDetail> ExamDetails { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExamDetail>().HasData(
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q001" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q002" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q003" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q004" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q005" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q006" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q007" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q008" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q009" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q010" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q011" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q012" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q013" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q014" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q015" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q016" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q017" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q018" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q019" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q020" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q021" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q022" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q023" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q024" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q025" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q026" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q027" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q028" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q029" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q030" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q031" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q032" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q033" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q034" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q035" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q036" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q037" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q038" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q039" },
        new ExamDetail { ExamId = "EXAM001", QuestionId = "Q040" }
    );
            modelBuilder.Entity<Question>().HasData(
        // Easy Questions (15 câu)
        new Question
        {
            QuestionId = "Q001",
            QuestionContent = "What is 2 + 2?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"3\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"4\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"6\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q002",
            QuestionContent = "What is 5 - 3?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"2\",\"isCorrect\":true},{\"id\":\"2\",\"content\":\"3\",\"isCorrect\":false},{\"id\":\"3\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"1\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q003",
            QuestionContent = "What is 4 × 2?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"8\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"10\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"12\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q004",
            QuestionContent = "What is 10 ÷ 2?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"2\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q005",
            QuestionContent = "What is 7 + 5?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"11\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"12\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"13\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"10\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q006",
            QuestionContent = "What is 9 - 4?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"2\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"3\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"3\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q007",
            QuestionContent = "What is 3 × 3?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"9\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"15\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q008",
            QuestionContent = "What is 12 ÷ 3?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"3\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"4\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"6\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q009",
            QuestionContent = "What is 6 + 8?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"14\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"16\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"18\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q010",
            QuestionContent = "What is 15 - 7?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"8\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"9\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"7\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q011",
            QuestionContent = "What is 2 × 5?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"10\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"15\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q012",
            QuestionContent = "What is 20 ÷ 4?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"7\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q013",
            QuestionContent = "What is 1 + 9?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"10\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"11\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"12\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q014",
            QuestionContent = "What is 8 - 2?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"6\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"7\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"4\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },
        new Question
        {
            QuestionId = "Q015",
            QuestionContent = "What is 5 × 2?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"10\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"15\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Easy"
        },

        // Medium Questions (15 câu)
        new Question
        {
            QuestionId = "Q016",
            QuestionContent = "What is 15 × 3?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"40\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"45\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"50\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"55\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q017",
            QuestionContent = "What is 36 ÷ 6?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"6\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"7\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"8\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q018",
            QuestionContent = "What is 12 + 18?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"28\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"30\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"32\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"34\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q019",
            QuestionContent = "What is 50 - 23?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"25\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"27\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"29\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"31\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q020",
            QuestionContent = "What is 7 × 8?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"54\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"56\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"58\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"60\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q021",
            QuestionContent = "What is 45 ÷ 9?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"7\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q022",
            QuestionContent = "What is 19 + 26?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"43\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"45\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"47\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"49\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q023",
            QuestionContent = "What is 72 - 38?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"32\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"34\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"36\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"38\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q024",
            QuestionContent = "What is 9 × 6?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"52\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"54\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"56\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"58\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q025",
            QuestionContent = "What is 80 ÷ 5?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"14\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"16\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"18\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"20\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q026",
            QuestionContent = "What is 13 + 29?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"40\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"42\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"44\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"46\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q027",
            QuestionContent = "What is 64 - 19?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"43\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"45\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"47\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"49\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q028",
            QuestionContent = "What is 11 × 4?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"42\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"44\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"46\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"48\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q029",
            QuestionContent = "What is 90 ÷ 6?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"13\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"15\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"17\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"19\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },
        new Question
        {
            QuestionId = "Q030",
            QuestionContent = "What is 25 + 17?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"40\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"42\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"44\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"46\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Medium"
        },

        // Hard Questions (10 câu)
        new Question
        {
            QuestionId = "Q031",
            QuestionContent = "What is 17 × 13?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"211\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"221\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"231\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"241\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q032",
            QuestionContent = "What is 144 ÷ 12?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"10\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"12\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"14\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"16\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q033",
            QuestionContent = "What is 56 + 78?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"132\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"134\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"136\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"138\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q034",
            QuestionContent = "What is 123 - 87?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"34\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"36\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"38\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"40\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q035",
            QuestionContent = "What is 19 × 7?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"131\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"133\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"135\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"137\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q036",
            QuestionContent = "What is 225 ÷ 15?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"13\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"15\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"17\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"19\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q037",
            QuestionContent = "What is 89 + 67?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"154\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"156\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"158\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"160\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q038",
            QuestionContent = "What is 176 - 98?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"76\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"78\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"80\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"82\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q039",
            QuestionContent = "What is 23 × 9?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"205\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"207\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"209\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"211\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        },
        new Question
        {
            QuestionId = "Q040",
            QuestionContent = "What is 300 ÷ 12?",
            AnswersJson = "[{\"id\":\"1\",\"content\":\"23\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"25\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"27\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"29\",\"isCorrect\":false}]",
            SubjectId = "SUB001",
            Difficulty = "Hard"
        }
    );
            // Giới hạn độ dài chuỗi cho các cột khóa
            modelBuilder.Entity<Exam>()
                .Property(e => e.ExamId)
                .HasMaxLength(200);

            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionId)
                .HasMaxLength(200);

            modelBuilder.Entity<ExamDetail>()
                .Property(ed => ed.ExamId)
                .HasMaxLength(200);

            modelBuilder.Entity<ExamDetail>()
                .Property(ed => ed.QuestionId)
                .HasMaxLength(200);

            modelBuilder.Entity<ExamResult>()
                .Property(er => er.ExamId)
                .HasMaxLength(200);

            modelBuilder.Entity<ExamResult>()
                .Property(er => er.UserId)
                .HasMaxLength(250);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasMaxLength(250);

            // Cấu hình khóa chính composite cho ExamDetail
            modelBuilder.Entity<ExamDetail>()
                .HasKey(ed => new { ed.ExamId, ed.QuestionId });

            // Cấu hình khóa chính composite cho ExamResult
            modelBuilder.Entity<ExamResult>()
                .HasKey(er => new { er.ExamId, er.UserId });

            // Cấu hình quan hệ giữa Exam và ExamDetail
            modelBuilder.Entity<ExamDetail>()
                .HasOne(ed => ed.Exam)
                .WithMany(e => e.ExamDetails)
                .HasForeignKey(ed => ed.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamDetail>()
                .HasOne(ed => ed.Question)
                .WithMany(q => q.ExamDetails)
                .HasForeignKey(ed => ed.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Cấu hình quan hệ giữa Exam và ExamResult
            modelBuilder.Entity<ExamResult>()
            .HasOne(er => er.Exam)
             .WithMany() // Không ánh xạ navigation property
             .HasForeignKey(er => er.ExamId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.User)
                .WithMany(u => u.ExamResults)
                .HasForeignKey(er => er.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ giữa Exam và Manager (User)
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Manager)
                .WithMany(u => u.ManagedExams)
                .HasForeignKey(e => e.ManagerId);

            // Cấu hình quan hệ giữa Course và User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Course)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CourseId);

            // Cấu hình quan hệ giữa Course và Subject
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.CourseId)
                .IsRequired(false);

            // Cấu hình quan hệ giữa Subject và Question
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Subject)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SubjectId);

            // Cấu hình quan hệ giữa Subject và Exam
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Subject)
                .WithMany()
                .HasForeignKey(e => e.SubjectId);
        }
    }
}