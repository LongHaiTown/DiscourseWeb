using EduquizSuper.Data;
using EduquizSuper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EduquizSuper.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduquizContextDb _context;

        public HomeController(EduquizContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 6; // Số khóa học hiển thị trên mỗi trang
            var coursesQuery = _context.Courses
                .Include(c => c.Subjects)
                .Include(c => c.Users)
                .AsNoTracking();

            var pagedCourses = await PaginatedList<Course>.CreateAsync(
                coursesQuery, pageNumber ?? 1, pageSize);

            ViewBag.PageIndex = pagedCourses.PageIndex;
            ViewBag.TotalPages = pagedCourses.TotalPages;

            return View(pagedCourses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}