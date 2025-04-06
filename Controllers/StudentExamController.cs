using EduquizSuper.Data;
using EduquizSuper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduquizSuper.Controllers
{
    //[Authorize(Roles = "Student")]
    public class StudentExamController : Controller
    {
        private readonly EduquizContextDb _context;
        private readonly UserManager<User> _userManager;

        public StudentExamController(EduquizContextDb context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StudentExam
        public async Task<IActionResult> Index(string subjectId, int? pageNumber)
        {
            int pageSize = 10;
            string currentUserId = _userManager.GetUserId(User);
            var currentUser = await _userManager.FindByIdAsync(currentUserId);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account"); // Hoặc xử lý theo yêu cầu của bạn
            }

            // Lấy danh sách kỳ thi có sẵn trong khoảng thời gian hợp lệ
            var examsQuery = _context.Exams
                .Include(e => e.Subject)
                .Where(e => e.StartTime <= DateTime.Now && e.EndTime >= DateTime.Now);

            // Nếu học viên có khoá học, lọc kỳ thi theo khóa học
            if (!string.IsNullOrEmpty(currentUser.CourseId))
            {
                var subjectsInCourse = await _context.Subjects
                    .Where(s => s.CourseId == currentUser.CourseId)
                    .Select(s => s.SubjectId)
                    .ToListAsync();

                examsQuery = examsQuery.Where(e => subjectsInCourse.Contains(e.SubjectId));
            }

            // Nếu có lọc theo môn học
            if (!string.IsNullOrEmpty(subjectId))
            {
                examsQuery = examsQuery.Where(e => e.SubjectId == subjectId);
            }

            // Lấy danh sách các kỳ thi mà học viên đã tham gia
            var takenExams = await _context.ExamResults
                .Where(er => er.UserId == currentUserId)
                .Select(er => er.ExamId)
                .ToListAsync();

            ViewData["TakenExams"] = takenExams;
            ViewData["CurrentSubject"] = subjectId;
            ViewData["SubjectId"] = new SelectList(
                await _context.Subjects.ToListAsync(), "SubjectId", "SubjectName");

            var exams = await PaginatedList<Exam>.CreateAsync(examsQuery.AsNoTracking(), pageNumber ?? 1, pageSize);
            return View(exams);
        }


        // GET: StudentExam/Start/5
        public async Task<IActionResult> Start(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            // Check if the exam exists and is available - use eager loading to get all related data
            var exam = await _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.ExamDetails)
                    .ThenInclude(ed => ed.Question)
                .FirstOrDefaultAsync(e => e.ExamId == id);

            if (exam == null)
            {
                return NotFound();
            }

            // Check if the exam is currently active
            if (DateTime.Now < exam.StartTime || DateTime.Now > exam.EndTime)
            {
                TempData["ErrorMessage"] = "This exam is not currently active.";
                return RedirectToAction(nameof(Index));
            }

            // Check if the student has already taken this exam
            var existingResult = await _context.ExamResults
                .FirstOrDefaultAsync(er => er.ExamId == id && er.UserId == currentUserId);

            if (existingResult != null)
            {
                TempData["InfoMessage"] = "You have already taken this exam.";
                return RedirectToAction(nameof(Result), new { id = id });
            }

            // Prepare the exam questions
            var examDetails = exam.ExamDetails.ToList();

            if (examDetails.Count == 0)
            {
                TempData["ErrorMessage"] = "No questions found for this exam.";
                return RedirectToAction(nameof(Index));
            }

            var questions = examDetails
                .Select(ed => ed.Question)
                .ToList();

            // Debug check to make sure we're loading questions
            Console.WriteLine($"Loaded {questions.Count} questions for exam {id}");

            foreach (var question in questions)
            {
                try
                {
                    // Parse the answers for each question
                    var answers = JsonConvert.DeserializeObject<List<dynamic>>(question.AnswersJson);

                    // Check if answers are valid
                    if (answers == null || answers.Count == 0)
                    {
                        Console.WriteLine($"Warning: Question {question.QuestionId} has no answers or invalid JSON");
                        continue;
                    }

                    // Randomize the order of the answers
                    var random = new Random();
                    var randomizedAnswers = answers.OrderBy(a => random.Next()).ToList();

                    // Store the randomized answers back
                    question.AnswersJson = JsonConvert.SerializeObject(randomizedAnswers);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing answers for question {question.QuestionId}: {ex.Message}");
                    // Provide a default answer format if there's an error
                    question.AnswersJson = JsonConvert.SerializeObject(new List<dynamic>());
                }
            }

            ViewData["Exam"] = exam;
            ViewData["StartTime"] = DateTime.Now;
            ViewData["QuestionsCount"] = questions.Count;

            return View(questions);
        }

        // POST: StudentExam/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(string examId, Dictionary<string, string> answers, DateTime startTime)
        {
            if (string.IsNullOrEmpty(examId) || answers == null)
            {
                return BadRequest();
            }

            var currentUserId = _userManager.GetUserId(User);

            var exam = await _context.Exams
                .Include(e => e.ExamDetails)
                    .ThenInclude(ed => ed.Question)
                .FirstOrDefaultAsync(e => e.ExamId == examId);

            if (exam == null)
            {
                return NotFound();
            }

            var existingResult = await _context.ExamResults
                .FirstOrDefaultAsync(er => er.ExamId == examId && er.UserId == currentUserId);

            if (existingResult != null)
            {
                return RedirectToAction(nameof(Result), new { id = examId });
            }

            // Tính điểm và cập nhật ExamDetails
            double score = 0;
            foreach (var detail in exam.ExamDetails)
            {
                if (answers.TryGetValue(detail.QuestionId, out string answerId))
                {
                    var correctAnswers = JsonConvert.DeserializeObject<List<dynamic>>(detail.Question.AnswersJson);
                    foreach (var answer in correctAnswers)
                    {
                        if (answer.id.ToString() == answerId && answer.isCorrect == true)
                        {
                            score += exam.TotalScore / exam.NumberOfQuestions;
                            detail.IsCorrect = true; // Cập nhật IsCorrect
                            break;
                        }
                    }
                }
            }

            // Lưu ExamResult
            var examResult = new ExamResult
            {
                ExamId = examId,
                UserId = currentUserId,
                Score = score,
                SubmitTime = DateTime.Now,
                Duration = DateTime.Now - startTime,
                UserAnswersJson = JsonConvert.SerializeObject(answers)
            };

            _context.ExamResults.Add(examResult);
            await _context.SaveChangesAsync(); // Lưu cả ExamDetails và ExamResult

            return RedirectToAction(nameof(Result), new { id = examId });
        }

        // GET: StudentExam/Result/5
        public async Task<IActionResult> Result(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            // Get the exam result
            var examResult = await _context.ExamResults
                .Include(er => er.Exam)
                    .ThenInclude(e => e.Subject)
                .Include(er => er.User)
                .FirstOrDefaultAsync(er => er.ExamId == id && er.UserId == currentUserId);

            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // GET: StudentExam/ReviewResult/5
        public async Task<IActionResult> ReviewResult(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            var examResult = await _context.ExamResults
                .Include(er => er.Exam)
                    .ThenInclude(e => e.Subject)
                .Include(er => er.User)
                .FirstOrDefaultAsync(er => er.ExamId == id && er.UserId == currentUserId);

            if (examResult == null)
            {
                return NotFound();
            }

            // Lấy danh sách câu hỏi của bài thi
            var questions = await _context.ExamDetails
                .Where(ed => ed.ExamId == id)
                .Include(ed => ed.Question)
                .Select(ed => ed.Question)
                .ToListAsync();

            ViewData["Questions"] = questions;

            return View(examResult);
        }

        // GET: StudentExam/Review/5
        public async Task<IActionResult> Review(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            // Get the exam result with questions and answers
            var examResult = await _context.ExamResults
                .Include(er => er.Exam)
                    .ThenInclude(e => e.ExamDetails)
                        .ThenInclude(ed => ed.Question)
                .Include(er => er.User)
                .FirstOrDefaultAsync(er => er.ExamId == id && er.UserId == currentUserId);

            if (examResult == null)
            {
                return NotFound();
            }

            // Check if review is allowed (based on exam configuration or time)
            if (DateTime.Now < examResult.Exam.EndTime)
            {
                // Don't allow review until the exam period ends for all students
                return RedirectToAction(nameof(Result), new { id = id });
            }

            // Parse the user's answers
            var userAnswers = JsonConvert.DeserializeObject<Dictionary<string, string>>(examResult.UserAnswersJson);
            ViewData["UserAnswers"] = userAnswers;

            return View(examResult);
        }
    }
}