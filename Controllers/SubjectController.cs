using EduquizSuper.Data;
using EduquizSuper.Models;
using EduquizSuper.Repositories;
using EduquizSuper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EduquizSuper.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IExamRepository _examRepository;

        // Thêm IExamRepository vào constructor
        public SubjectController(ISubjectRepository subjectRepository, ICourseRepository courseRepository, IExamRepository examRepository)
        {
            _subjectRepository = subjectRepository;
            _courseRepository = courseRepository;
            _examRepository = examRepository;
        }

        // GET: Subject
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber, string courseId = null)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCourse"] = courseId;

            ViewData["Courses"] = new SelectList(await _courseRepository.GetAllAsync(), "CourseId", "CourseName");

            int pageSize = 10;
            var subjects = await _subjectRepository.GetPaginatedAsync(searchString, courseId, pageNumber ?? 1, pageSize);
            return View(subjects);
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Lấy Subject cùng với chi tiết (bao gồm Course và Questions)
            var subject = await _subjectRepository.GetByIdWithDetailsAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            // Tính toán số lượng câu hỏi theo độ khó
            ViewData["EasyQuestionsCount"] = subject.Questions?.Count(q => q.Difficulty == "Easy") ?? 0;
            ViewData["MediumQuestionsCount"] = subject.Questions?.Count(q => q.Difficulty == "Medium") ?? 0;
            ViewData["HardQuestionsCount"] = subject.Questions?.Count(q => q.Difficulty == "Hard") ?? 0;

            return View(subject);
        }

        // GET: Subject/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var courses = await _courseRepository.GetAllAsync();
            ViewData["CourseId"] = new SelectList(courses, "CourseId", "CourseName", courses.Any() ? courses.First().CourseId : null);
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("SubjectId,SubjectName,CourseId")] Subject subject)
        {
            Console.WriteLine($"SubjectId: {subject.SubjectId}");
            Console.WriteLine($"SubjectName: {subject.SubjectName}");
            Console.WriteLine($"CourseId: {subject.CourseId}");

            if (!string.IsNullOrEmpty(subject.CourseId) && !await _courseRepository.ExistsAsync(subject.CourseId))
            {
                ModelState.AddModelError("CourseId", "Khóa học không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectRepository.AddAsync(subject);
                    await _subjectRepository.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Lỗi khi lưu: {ex.Message}");
                }
            }

            ViewData["CourseId"] = new SelectList(await _courseRepository.GetAllAsync(), "CourseId", "CourseName", subject.CourseId);
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(await _courseRepository.GetAllAsync(), "CourseId", "CourseName", subject.CourseId);
            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("SubjectId,SubjectName,CourseId")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _subjectRepository.Update(subject);
                    await _subjectRepository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!await _subjectRepository.ExistsAsync(subject.SubjectId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(await _courseRepository.GetAllAsync(), "CourseId", "CourseName", subject.CourseId);
            return View(subject);
        }

        // GET: Subject/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _subjectRepository.GetByIdWithDetailsAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);

            if (await _subjectRepository.HasRelatedQuestionsAsync(id))
            {
                ModelState.AddModelError(string.Empty, "Không thể xóa môn học này vì có câu hỏi liên quan. Hãy xóa các câu hỏi trước.");
                var subjectWithCourse = await _subjectRepository.GetByIdWithDetailsAsync(id);
                return View(subjectWithCourse);
            }

            if (subject != null)
            {
                _subjectRepository.Delete(subject);
                await _subjectRepository.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Subject/Questions/5
        public async Task<IActionResult> ShowQuestions(string id, string searchString, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _subjectRepository.GetByIdWithDetailsAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            ViewData["SubjectId"] = id;
            ViewData["SubjectName"] = subject.SubjectName;
            ViewData["CurrentFilter"] = searchString;

            int pageSize = 10;
            var questions = await _subjectRepository.GetQuestionsPaginatedAsync(id, searchString, pageNumber ?? 1, pageSize);
            return View(questions);
        }
    }
}