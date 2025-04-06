//using EduquizSuper.Data;
//using EduquizSuper.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EduquizSuper.Controllers
//{
//    [Authorize(Roles = "Admin")]
//    public class AdminExamController : Controller
//    {
//        private readonly EduquizContextDb _context;
//        private readonly UserManager<User> _userManager;

//        public AdminExamController(EduquizContextDb context, UserManager<User> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        // GET: AdminExam
//        public async Task<IActionResult> Index(int? pageNumber)
//        {
//            int pageSize = 10;
//            var exams = _context.Exams
//                .Include(e => e.Subject)
//                .Include(e => e.Manager)
//                .OrderByDescending(e => e.StartTime);

//            return View(await PaginatedList<Exam>.CreateAsync(exams.AsNoTracking(), pageNumber ?? 1, pageSize));
//        }

//        // GET: AdminExam/Details/5
//        public async Task<IActionResult> Details(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var exam = await _context.Exams
//                .Include(e => e.Subject)
//                .Include(e => e.Manager)
//                .Include(e => e.ExamDetails)
//                    .ThenInclude(ed => ed.Question)
//                .FirstOrDefaultAsync(m => m.ExamId == id);

//            if (exam == null)
//            {
//                return NotFound();
//            }

//            return View(exam);
//        }

//        // GET: AdminExam/Create
//        public async Task<IActionResult> Create()
//        {
//            ViewData["SubjectId"] = new SelectList(await _context.Subjects.ToListAsync(), "SubjectId", "SubjectName");
//            return View();
//        }

//        // POST: AdminExam/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("ExamId,ExamName,StartTime,EndTime,EasyQuestions,MediumQuestions,HardQuestions,ScorePerQuestion,SubjectId")] Exam exam)
//        {
//            // Debug: Log dữ liệu gửi từ form
//            Console.WriteLine("--- Debug Create POST ---");
//            Console.WriteLine($"ExamId: {exam.ExamId}");
//            Console.WriteLine($"ExamName: {exam.ExamName}");
//            Console.WriteLine($"StartTime: {exam.StartTime}");
//            Console.WriteLine($"EndTime: {exam.EndTime}");
//            Console.WriteLine($"EasyQuestions: {exam.EasyQuestions}");
//            Console.WriteLine($"MediumQuestions: {exam.MediumQuestions}");
//            Console.WriteLine($"HardQuestions: {exam.HardQuestions}");
//            Console.WriteLine($"ScorePerQuestion: {exam.ScorePerQuestion}");
//            Console.WriteLine($"SubjectId: {exam.SubjectId}");

//            // Xóa lỗi không cần thiết từ ModelState
//            if (ModelState.ContainsKey("ManagerId"))
//            {
//                ModelState["ManagerId"].Errors.Clear();
//                ModelState["ManagerId"].ValidationState = ModelValidationState.Valid;
//            }
//            if (ModelState.ContainsKey("ExamDetails"))
//            {
//                ModelState["ExamDetails"].Errors.Clear();
//                ModelState["ExamDetails"].ValidationState = ModelValidationState.Valid;
//            }

//            if (ModelState.IsValid)
//            {
//                // Gán giá trị mới cho ExamId nếu rỗng
//                if (string.IsNullOrEmpty(exam.ExamId))
//                {
//                    exam.ExamId = Guid.NewGuid().ToString();
//                    Console.WriteLine($"生成的 ExamId: {exam.ExamId}");
//                }

//                // Gán ManagerId từ người dùng hiện tại
//                var userId = _userManager.GetUserId(User);
//                if (string.IsNullOrEmpty(userId))
//                {
//                    Console.WriteLine("Error: User not authenticated or no UserId found.");
//                    ModelState.AddModelError("", "Không thể xác định người quản lý. Vui lòng đăng nhập lại.");
//                }
//                else
//                {
//                    exam.ManagerId = userId;
//                    Console.WriteLine($"ManagerId: {exam.ManagerId}");
//                }

//                // Tính toán NumberOfQuestions và TotalScore
//                exam.NumberOfQuestions = exam.EasyQuestions + exam.MediumQuestions + exam.HardQuestions;
//                exam.TotalScore = exam.NumberOfQuestions * exam.ScorePerQuestion;
//                Console.WriteLine($"Calculated NumberOfQuestions: {exam.NumberOfQuestions}");
//                Console.WriteLine($"Calculated TotalScore: {exam.TotalScore}");

//                // Kiểm tra tổng số câu hỏi
//                if (exam.NumberOfQuestions <= 0)
//                {
//                    ModelState.AddModelError("", "Tổng số câu hỏi phải lớn hơn 0.");
//                    Console.WriteLine("Error: Tổng số câu hỏi <= 0");
//                }

//                if (ModelState.IsValid)
//                {
//                    try
//                    {
//                        _context.Add(exam);
//                        await _context.SaveChangesAsync();
//                        Console.WriteLine("Exam saved successfully");
//                        return RedirectToAction(nameof(ManageQuestions), new { id = exam.ExamId });
//                    }
//                    catch (Exception ex)
//                    {
//                        ModelState.AddModelError("", $"Lỗi khi lưu bài thi: {ex.Message}");
//                        Console.WriteLine($"Exception: {ex.Message}");
//                        Console.WriteLine($"StackTrace: {ex.StackTrace}");
//                    }
//                }
//            }

//            // Debug: Log lỗi từ ModelState nếu không hợp lệ
//            if (!ModelState.IsValid)
//            {
//                Console.WriteLine("--- ModelState Errors ---");
//                foreach (var state in ModelState)
//                {
//                    if (state.Value.Errors.Any())
//                    {
//                        Console.WriteLine($"Key: {state.Key}");
//                        foreach (var error in state.Value.Errors)
//                        {
//                            Console.WriteLine($"Error: {error.ErrorMessage}");
//                        }
//                    }
//                }
//            }

//            ViewData["SubjectId"] = new SelectList(await _context.Subjects.ToListAsync(), "SubjectId", "SubjectName", exam.SubjectId);
//            return View(exam);
//        }
//        // GET: AdminExam/Edit/5
//        public async Task<IActionResult> Edit(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var exam = await _context.Exams.FindAsync(id);
//            if (exam == null)
//            {
//                return NotFound();
//            }

//            ViewData["SubjectId"] = new SelectList(await _context.Subjects.ToListAsync(), "SubjectId", "SubjectName", exam.SubjectId);
//            return View(exam);
//        }

//        // POST: AdminExam/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(string id, [Bind("ExamId,ExamName,StartTime,EndTime,NumberOfQuestions,TotalScore,SubjectId,ManagerId")] Exam exam)
//        {
//            if (id != exam.ExamId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(exam);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ExamExists(exam.ExamId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }

//            ViewData["SubjectId"] = new SelectList(await _context.Subjects.ToListAsync(), "SubjectId", "SubjectName", exam.SubjectId);
//            return View(exam);
//        }

//        // GET: AdminExam/Delete/5
//        public async Task<IActionResult> Delete(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var exam = await _context.Exams
//                .Include(e => e.Subject)
//                .Include(e => e.Manager)
//                .FirstOrDefaultAsync(m => m.ExamId == id);

//            if (exam == null)
//            {
//                return NotFound();
//            }

//            return View(exam);
//        }

//        // POST: AdminExam/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(string id)
//        {
//            var exam = await _context.Exams.FindAsync(id);
//            if (exam != null)
//            {
//                _context.Exams.Remove(exam);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        // GET: AdminExam/ManageQuestions/5
//        public async Task<IActionResult> ManageQuestions(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var exam = await _context.Exams
//                .Include(e => e.Subject)
//                .Include(e => e.ExamDetails)
//                    .ThenInclude(ed => ed.Question)
//                .FirstOrDefaultAsync(m => m.ExamId == id);

//            if (exam == null)
//            {
//                return NotFound();
//            }

//            // Lấy danh sách câu hỏi của môn học
//            var questions = await _context.Questions
//                .Where(q => q.SubjectId == exam.SubjectId)
//                .ToListAsync();

//            // Tạo danh sách câu hỏi đã được chọn
//            var selectedQuestionIds = exam.ExamDetails
//                .Select(ed => ed.QuestionId)
//                .ToList();

//            ViewData["Exam"] = exam;
//            ViewData["Questions"] = questions;
//            ViewData["SelectedQuestionIds"] = selectedQuestionIds;

//            return View();
//        }

//        // POST: AdminExam/UpdateExamQuestions
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UpdateExamQuestions(string examId, List<string> selectedQuestions)
//        {
//            if (string.IsNullOrEmpty(examId))
//            {
//                return BadRequest();
//            }

//            var exam = await _context.Exams.FindAsync(examId);
//            if (exam == null)
//            {
//                return NotFound();
//            }

//            var existingDetails = await _context.ExamDetails
//                .Where(ed => ed.ExamId == examId)
//                .ToListAsync();
//            _context.ExamDetails.RemoveRange(existingDetails);

//            if (selectedQuestions != null && selectedQuestions.Any())
//            {
//                var questions = await _context.Questions
//                    .Where(q => selectedQuestions.Contains(q.QuestionId))
//                    .ToListAsync();

//                exam.EasyQuestions = questions.Count(q => q.Difficulty == "Easy");
//                exam.MediumQuestions = questions.Count(q => q.Difficulty == "Medium");
//                exam.HardQuestions = questions.Count(q => q.Difficulty == "Hard");

//                foreach (var questionId in selectedQuestions)
//                {
//                    _context.ExamDetails.Add(new ExamDetail
//                    {
//                        ExamId = examId,
//                        QuestionId = questionId
//                    });
//                }
//                _context.Update(exam);
//            }
//            else
//            {
//                exam.EasyQuestions = 0;
//                exam.MediumQuestions = 0;
//                exam.HardQuestions = 0;
//                _context.Update(exam);
//            }

//            await _context.SaveChangesAsync();
//            TempData["SuccessMessage"] = "Cập nhật danh sách câu hỏi thành công!";
//            return RedirectToAction(nameof(Details), new { id = examId });
//        }

//        // POST: AdminExam/GenerateRandomQuestions
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> GenerateRandomQuestions(string examId, int numberOfQuestions)
//        //{
//        //    if (string.IsNullOrEmpty(examId) || numberOfQuestions <= 0)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    var exam = await _context.Exams.FindAsync(examId);
//        //    if (exam == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    // Lấy danh sách câu hỏi ngẫu nhiên từ môn học
//        //    var randomQuestions = await _context.Questions
//        //        .Where(q => q.SubjectId == exam.SubjectId)
//        //        .OrderBy(q => Guid.NewGuid())
//        //        .Take(numberOfQuestions)
//        //        .ToListAsync();

//        //    if (randomQuestions.Count == 0)
//        //    {
//        //        TempData["ErrorMessage"] = "Không có câu hỏi nào được tìm thấy cho môn học này.";
//        //        return RedirectToAction(nameof(ManageQuestions), new { id = examId });
//        //    }

//        //    // Xóa các liên kết câu hỏi cũ
//        //    var existingDetails = await _context.ExamDetails
//        //        .Where(ed => ed.ExamId == examId)
//        //        .ToListAsync();

//        //    _context.ExamDetails.RemoveRange(existingDetails);

//        //    // Thêm các câu hỏi ngẫu nhiên
//        //    foreach (var question in randomQuestions)
//        //    {
//        //        _context.ExamDetails.Add(new ExamDetail
//        //        {
//        //            ExamId = examId,
//        //            QuestionId = question.QuestionId
//        //        });
//        //    }

//        //    // Cập nhật số lượng câu hỏi
//        //    exam.NumberOfQuestions = randomQuestions.Count;
//        //    _context.Update(exam);

//        //    await _context.SaveChangesAsync();
//        //    TempData["SuccessMessage"] = $"Đã tạo ngẫu nhiên {randomQuestions.Count} câu hỏi cho bài thi.";
//        //    return RedirectToAction(nameof(ManageQuestions), new { id = examId });
//        //}

//        private bool ExamExists(string id)
//        {
//            return _context.Exams.Any(e => e.ExamId == id);
//        }
//    }
//}
using EduquizSuper.Models;
using EduquizSuper.Repositories;
using EduquizSuper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduquizSuper.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminExamController : Controller
    {
        private readonly IExamRepository _examRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IExamDetailRepository _examDetailRepository;
        private readonly UserManager<User> _userManager;

        public AdminExamController(
            IExamRepository examRepository,
            ISubjectRepository subjectRepository,
            IQuestionRepository questionRepository,
            IExamDetailRepository examDetailRepository,
            UserManager<User> userManager)
        {
            _examRepository = examRepository;
            _subjectRepository = subjectRepository;
            _questionRepository = questionRepository;
            _examDetailRepository = examDetailRepository;
            _userManager = userManager;
        }

        // GET: AdminExam
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var exams = await _examRepository.GetPaginatedAsync(pageNumber ?? 1, pageSize);
            return View(exams);
        }

        // GET: AdminExam/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _examRepository.GetByIdWithDetailsAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: AdminExam/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SubjectId"] = new SelectList(await _subjectRepository.GetAllAsync(), "SubjectId", "SubjectName");
            return View();
        }

        // POST: AdminExam/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamId,ExamName,StartTime,EndTime,EasyQuestions,MediumQuestions,HardQuestions,ScorePerQuestion,SubjectId")] Exam exam)
        {
            Console.WriteLine("--- Debug Create POST ---");
            Console.WriteLine($"ExamId: {exam.ExamId}");
            Console.WriteLine($"ExamName: {exam.ExamName}");
            Console.WriteLine($"StartTime: {exam.StartTime}");
            Console.WriteLine($"EndTime: {exam.EndTime}");
            Console.WriteLine($"EasyQuestions: {exam.EasyQuestions}");
            Console.WriteLine($"MediumQuestions: {exam.MediumQuestions}");
            Console.WriteLine($"HardQuestions: {exam.HardQuestions}");
            Console.WriteLine($"ScorePerQuestion: {exam.ScorePerQuestion}");
            Console.WriteLine($"SubjectId: {exam.SubjectId}");

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(exam.ExamId))
                {
                    exam.ExamId = Guid.NewGuid().ToString();
                    Console.WriteLine($"Generated ExamId: {exam.ExamId}");
                }

                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    Console.WriteLine("Error: User not authenticated or no UserId found.");
                    ModelState.AddModelError("", "Không thể xác định người quản lý. Vui lòng đăng nhập lại.");
                }
                else
                {
                    exam.ManagerId = userId;
                    Console.WriteLine($"ManagerId: {exam.ManagerId}");
                }

                exam.NumberOfQuestions = exam.EasyQuestions + exam.MediumQuestions + exam.HardQuestions;
                exam.TotalScore = exam.NumberOfQuestions * exam.ScorePerQuestion;
                Console.WriteLine($"Calculated NumberOfQuestions: {exam.NumberOfQuestions}");
                Console.WriteLine($"Calculated TotalScore: {exam.TotalScore}");

                if (exam.NumberOfQuestions <= 0)
                {
                    ModelState.AddModelError("", "Tổng số câu hỏi phải lớn hơn 0.");
                    Console.WriteLine("Error: Tổng số câu hỏi <= 0");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _examRepository.AddAsync(exam);
                        await _examRepository.SaveChangesAsync();
                        Console.WriteLine("Exam saved successfully");
                        return RedirectToAction(nameof(ManageQuestions), new { id = exam.ExamId });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Lỗi khi lưu bài thi: {ex.Message}");
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }

            ViewData["SubjectId"] = new SelectList(await _subjectRepository.GetAllAsync(), "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // GET: AdminExam/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            ViewData["SubjectId"] = new SelectList(await _subjectRepository.GetAllAsync(), "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // POST: AdminExam/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExamId,ExamName,StartTime,EndTime,NumberOfQuestions,TotalScore,SubjectId,ManagerId")] Exam exam)
        {
            if (id != exam.ExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _examRepository.Update(exam);
                    await _examRepository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!await _examRepository.ExistsAsync(exam.ExamId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["SubjectId"] = new SelectList(await _subjectRepository.GetAllAsync(), "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // GET: AdminExam/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _examRepository.GetByIdWithDetailsAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: AdminExam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam != null)
            {
                _examRepository.Delete(exam);
                await _examRepository.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminExam/ManageQuestions/5
        public async Task<IActionResult> ManageQuestions(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _examRepository.GetByIdWithDetailsAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            var questions = await _questionRepository.GetBySubjectIdAsync(exam.SubjectId);
            var selectedQuestionIds = exam.ExamDetails.Select(ed => ed.QuestionId).ToList();

            ViewData["Exam"] = exam;
            ViewData["Questions"] = questions;
            ViewData["SelectedQuestionIds"] = selectedQuestionIds;

            return View();
        }

        // POST: AdminExam/UpdateExamQuestions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateExamQuestions(string examId, List<string> selectedQuestions)
        {
            if (string.IsNullOrEmpty(examId))
            {
                return BadRequest();
            }

            var exam = await _examRepository.GetByIdAsync(examId);
            if (exam == null)
            {
                return NotFound();
            }

            var existingDetails = await _examDetailRepository.GetByExamIdAsync(examId);
            await _examDetailRepository.RemoveRangeAsync(existingDetails);

            if (selectedQuestions != null && selectedQuestions.Any())
            {
                var questions = await _questionRepository.GetByIdsAsync(selectedQuestions);
                exam.EasyQuestions = questions.Count(q => q.Difficulty == "Easy");
                exam.MediumQuestions = questions.Count(q => q.Difficulty == "Medium");
                exam.HardQuestions = questions.Count(q => q.Difficulty == "Hard");

                var newDetails = selectedQuestions.Select(qid => new ExamDetail
                {
                    ExamId = examId,
                    QuestionId = qid
                });
                await _examDetailRepository.AddRangeAsync(newDetails);
                _examRepository.Update(exam);
            }
            else
            {
                exam.EasyQuestions = 0;
                exam.MediumQuestions = 0;
                exam.HardQuestions = 0;
                _examRepository.Update(exam);
            }

            await _examDetailRepository.SaveChangesAsync();
            await _examRepository.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật danh sách câu hỏi thành công!";
            return RedirectToAction(nameof(Details), new { id = examId });
        }
    }
}