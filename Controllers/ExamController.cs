using EduquizSuper.Data;
using EduquizSuper.Models;
using EduquizSuper.Repositories;
using EduquizSuper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduquizSuper.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly IExamRepository _examRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly UserManager<User> _userManager;
        private readonly EduquizContextDb _context;

        public ExamController(
            IExamRepository examRepository,
            ISubjectRepository subjectRepository,
            UserManager<User> userManager,
            EduquizContextDb context)
        {
            _examRepository = examRepository;
            _subjectRepository = subjectRepository;
            _userManager = userManager;
            _context = context;
        }

        // GET: Exam
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            return View(await _examRepository.GetPaginatedAsync(pageNumber ?? 1, pageSize));
        }

        // GET: Exam/Details/5
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

        // GET: Exam/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var availableSubjects = await _subjectRepository.GetAllAsync();
            ViewBag.SubjectId = new SelectList(availableSubjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: Exam/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra ExamId đã tồn tại chưa
                if (await _examRepository.ExistsAsync(exam.ExamId))
                {
                    ModelState.AddModelError("ExamId", "Mã kỳ thi này đã tồn tại.");
                    var subjects = await _subjectRepository.GetAllAsync();
                    ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", exam.SubjectId);
                    return View(exam);
                }
        
            // Điều chỉnh thời gian để kỳ thi hiển thị ngay

            Ascending: if (exam.StartTime > DateTime.Now)
                {
                    exam.StartTime = DateTime.Now;
                }
                if (exam.EndTime <= DateTime.Now)
                {
                    exam.EndTime = DateTime.Now.AddMinutes(exam.Duration);
                }
                if (exam.StartTime >= exam.EndTime)
                {
                    ModelState.AddModelError("EndTime", "Thời gian kết thúc phải sau thời gian bắt đầu.");
                    var subjects = await _subjectRepository.GetAllAsync();
                    ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", exam.SubjectId);
                    return View(exam);
                }

                // Kiểm tra SubjectId
                if (!await _subjectRepository.ExistsAsync(exam.SubjectId))
                {
                    ModelState.AddModelError("SubjectId", "Môn học không tồn tại.");
                    var subjects = await _subjectRepository.GetAllAsync();
                    ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", exam.SubjectId);
                    return View(exam);
                }

                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    exam.ManagerId = user.Id;
                }
                else
                {
                    // Xử lý lỗi hoặc gán giá trị mặc định
                    ModelState.AddModelError("", "Không thể xác định người tạo bài thi.");
                    // Render lại view với thông báo lỗi
                    var subjects = await _subjectRepository.GetAllAsync();
                    ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", exam.SubjectId);
                    return View(exam);
                }
                user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    exam.ManagerId = user.Id;
                }
                else
                {
                    // Xử lý lỗi hoặc gán giá trị mặc định
                    ModelState.AddModelError("", "Không thể xác định người tạo bài thi.");
                    // Render lại view với thông báo lỗi
                    var subjects = await _subjectRepository.GetAllAsync();
                    ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", exam.SubjectId);
                    return View(exam);
                }
                exam.NumberOfQuestions = exam.EasyQuestions + exam.MediumQuestions + exam.HardQuestions;
                exam.TotalScore = exam.NumberOfQuestions * exam.ScorePerQuestion;

                // Lưu kỳ thi
                await _examRepository.AddAsync(exam);
                await _examRepository.SaveChangesAsync();
                if (_context == null || _context.Questions == null)
                {
                    ModelState.AddModelError("", "Không thể truy cập cơ sở dữ liệu câu hỏi.");
                    var subjects = await _subjectRepository.GetAllAsync();
                    ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", exam.SubjectId);
                    return View(exam);
                }
                // Thêm câu hỏi vào ExamDetails theo độ khó
                try
                {
                    var easyQuestions = await _context.Questions
                        .Where(q => q.SubjectId == exam.SubjectId && q.Difficulty == "Easy")
                        .OrderBy(q => Guid.NewGuid()) // Ngẫu nhiên
                        .Take(exam.EasyQuestions)
                    .ToListAsync();

                    var mediumQuestions = await _context.Questions
                        .Where(q => q.SubjectId == exam.SubjectId && q.Difficulty == "Medium")
                        .OrderBy(q => Guid.NewGuid())
                        .Take(exam.MediumQuestions)
                    .ToListAsync();

                    var hardQuestions = await _context.Questions
                        .Where(q => q.SubjectId == exam.SubjectId && q.Difficulty == "Hard")
                        .OrderBy(q => Guid.NewGuid())
                        .Take(exam.HardQuestions)
                        .ToListAsync();

                    // Kiểm tra xem có đủ câu hỏi không
                    if (easyQuestions.Count < exam.EasyQuestions)
                    {
                        ModelState.AddModelError("EasyQuestions", $"Không đủ {exam.EasyQuestions} câu hỏi dễ. Chỉ có {easyQuestions.Count} câu.");
                        throw new Exception("Không đủ câu hỏi");
                    }
                    if (mediumQuestions.Count < exam.MediumQuestions)
                    {
                        ModelState.AddModelError("MediumQuestions", $"Không đủ {exam.MediumQuestions} câu hỏi trung bình. Chỉ có {mediumQuestions.Count} câu.");
                        throw new Exception("Không đủ câu hỏi");
                    }
                    if (hardQuestions.Count < exam.HardQuestions)
                    {
                        ModelState.AddModelError("HardQuestions", $"Không đủ {exam.HardQuestions} câu hỏi khó. Chỉ có {hardQuestions.Count} câu.");
                        throw new Exception("Không đủ câu hỏi");
                    }

                    // Gộp danh sách câu hỏi
                    var selectedQuestions = easyQuestions
                        .Concat(mediumQuestions)
                        .Concat(hardQuestions)
                        .ToList();

                    Console.WriteLine($"Selected {selectedQuestions.Count} questions for ExamId {exam.ExamId}");

                    // Tạo ExamDetails
                    var examDetails = selectedQuestions.Select(q => new ExamDetail
                    {
                        ExamId = exam.ExamId,
                        QuestionId = q.QuestionId,
                        IsCorrect = false // Giá trị mặc định
                    }).ToList();

                    _context.ExamDetails.AddRange(examDetails);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi (ví dụ: không đủ câu hỏi), xóa kỳ thi vừa tạo và trả về lỗi
                    _examRepository.Delete(exam);
                    await _examRepository.SaveChangesAsync();

                    Console.WriteLine($"Error adding ExamDetails: {ex.Message}");
                    var subjects = await _subjectRepository.GetAllAsync();
                    ViewBag.SubjectId = new SelectList(subjects, "SubjectId", "SubjectName", exam.SubjectId);
                    return View(exam);
                }

                return RedirectToAction(nameof(Index));
            }

            var availableSubjects = await _subjectRepository.GetAllAsync();
            ViewBag.SubjectId = new SelectList(availableSubjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }
        // GET: Exam/Edit/5
        [Authorize(Roles = "Admin")]
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

            var availableSubjects = await _subjectRepository.GetAllAsync();
            ViewBag.SubjectId = new SelectList(availableSubjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // POST: Exam/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, Exam exam)
        {
            if (id != exam.ExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!await _examRepository.ExistsAsync(id))
                {
                    return NotFound();
                }

                exam.NumberOfQuestions = exam.EasyQuestions + exam.MediumQuestions + exam.HardQuestions;
                exam.TotalScore = exam.NumberOfQuestions * exam.ScorePerQuestion;

                _examRepository.Update(exam);
                await _examRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var availableSubjects = await _subjectRepository.GetAllAsync();
            ViewBag.SubjectId = new SelectList(availableSubjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // GET: Exam/Delete/5
        [Authorize(Roles = "Admin")]
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

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _examRepository.Delete(exam);
            await _examRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}