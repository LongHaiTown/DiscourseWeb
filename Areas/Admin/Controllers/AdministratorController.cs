using DisCourse.Models;
using DisCourse.Repository;
using DisCourseW.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using DisCourseW.Areas.Admin.Models;

namespace DisCourseW.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Đảm bảo chỉ Admin truy cập được
    public class AdministratorController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserCourseRepository _userCourseRepository;

        public AdministratorController(ICourseRepository courseRepository,
            IUserRepository userRepository,
            IPostRepository postRepository,
            IUserCourseRepository userCourseRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _userCourseRepository = userCourseRepository;
        }

        // 📌 Hiển thị danh sách Course, Post và User
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllAsync();
            var posts = await _postRepository.GetAllAsync();
            var users = await _userRepository.GetAllUsersAsync(); // Giả định có phương thức GetAllAsync trong IUserRepository

            var viewModel = new CoursePostView
            {
                Courses = courses,
                Posts = posts,
                Users = users
            };
            return View(viewModel);
        }
    }
}