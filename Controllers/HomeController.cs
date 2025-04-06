using System.Diagnostics;
using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace DisCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPostRepository _postRepository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ICourseRepository courseRepository, IPostRepository postRepository, ILogger<HomeController> logger)
        {
            _courseRepository = courseRepository;
            _postRepository = postRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // L?y t?t c? c�c b�i vi?t
            var post = await _postRepository.GetAllAsync();

            // L?y 5 kh�a h?c h�ng ??u
            var topCourses = _courseRepository.GetTopCourses(5);

            // Truy?n danh s�ch kh�a h?c v�o ViewBag
            ViewBag.PopularCourses = topCourses;

            // Tr? v? view v?i c? hai d? li?u
            return View(post);
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
