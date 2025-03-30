using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
namespace DisCourse.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // 📌 Hiển thị danh sách Course
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllAsync();
            return View(courses);
        }

        // 📌 Hiển thị chi tiết một Course
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // 📌 Hiển thị form tạo Course
        public IActionResult Create()
        {
            return View();
        }

        // 📌 Xử lý tạo Course
        [HttpPost]
        public async Task<IActionResult> Create(Course course, IFormFile? ThumbnailFile)
        {
            if (ModelState.IsValid)
            {
                if (ThumbnailFile != null)
                {
                    course.Thumbnail = await SaveImage(ThumbnailFile);
                }

                await _courseRepository.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // Hàm lưu ảnh vào thư mục wwwroot/images
        private async Task<string?> SaveImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null; // Trả về null nếu không có file

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Tạo tên file duy nhất
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName; // Trả về đường dẫn lưu vào database
        }
        // GET: Hiển thị form chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Cập nhật thông tin khóa học
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, IFormFile? ThumbnailFile)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(course);
            }

            // Nếu có ảnh mới được upload, lưu ảnh và cập nhật đường dẫn
            if (ThumbnailFile != null)
            {
                var imagePath = await SaveImage(ThumbnailFile);
                course.Thumbnail = imagePath;
            }

            await _courseRepository.UpdateAsync(course);
            return RedirectToAction(nameof(Index));
        }

        // 📌 Hiển thị form xác nhận xóa
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // 📌 Xử lý xóa Course
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
