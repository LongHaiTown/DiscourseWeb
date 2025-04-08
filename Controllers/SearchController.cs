using DisCourse.Models;
using DisCourseW.Models;

using DisCourse.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DisCourseW.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICourseRepository _courseRepository;

        public SearchController(IPostRepository postRepository, ICourseRepository courseRepository)
        {
            _postRepository = postRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index(string query)
        {
            // Lấy tất cả dữ liệu
            var posts = await _postRepository.GetAllAsync();
            var courses = await _courseRepository.GetAllAsync();

            // Nếu có query, lọc kết quả
            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                posts = posts.Where(p => p.Title.ToLower().Contains(query) ||
                                        p.Summary?.ToLower().Contains(query) == true ||
                                        p.Content.ToLower().Contains(query) ||
                                        p.Author.UserName.ToLower().Contains(query) ||
                                        p.Course.Name.ToLower().Contains(query))
                            .ToList();

                courses = courses.Where(c => c.Name.ToLower().Contains(query) ||
                                            c.Description?.ToLower().Contains(query) == true)
                                .ToList();
            }

            // Tạo view model để chứa kết quả
            var viewModel = new SearchResultsViewModel
            {
                Posts = posts,
                Courses = courses,
                Query = query
            };

            // Lưu query để hiển thị lại trong form
            ViewBag.SearchQuery = query;

            return View(viewModel);
        }

    }
}
