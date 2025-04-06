using EduquizSuper.Data;
using EduquizSuper.Models;
using EduquizSuper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduquizSuper.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: Course
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            var pageSize = 10;
            var courses = await _courseRepository.GetCoursesAsync(searchString, pageNumber ?? 1, pageSize);
            return View(courses);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // GET: Course/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,Description")] Course course)
        {
            if (await _courseRepository.CourseExistsAsync(course.CourseId))
            {
                ModelState.AddModelError("CourseId", "Mã khóa học đã tồn tại. Vui lòng chọn mã khác.");
            }

            if (ModelState.IsValid)
            {
                await _courseRepository.AddCourseAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("CourseId,CourseName,Description")] Course course)
        {
            if (id != course.CourseId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseRepository.UpdateCourseAsync(course);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _courseRepository.CourseExistsAsync(course.CourseId)) return NotFound();
                    throw;
                }
            }
            return View(course);
        }

        // GET: Course/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _courseRepository.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}