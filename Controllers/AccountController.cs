using EduquizSuper.Data;
using EduquizSuper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EduquizSuper.ViewModels;
namespace EduquizSuper.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EduquizContextDb _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            EduquizContextDb context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin.");
                    return View(model);
                }
            }
            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var courses = _context.Courses.Select(c => new SelectListItem
            {
                Value = c.CourseId,
                Text = c.CourseName
            }).ToList();

            ViewData["Courses"] = courses;
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Mặc định, người dùng mới đăng ký sẽ có Role là "Student"
                    await _userManager.AddToRoleAsync(user, "Student");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Profile
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                CurrentAvatar = user.Avatar,
                CourseId = user.CourseId
            };

            var courses = _context.Courses.Select(c => new SelectListItem
            {
                Value = c.CourseId,
                Text = c.CourseName,
                Selected = c.CourseId == user.CourseId
            }).ToList();

            ViewData["Courses"] = courses;
            return View(model);
        }

        // POST: Account/Profile
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var courses = _context.Courses.Select(c => new SelectListItem
                {
                    Value = c.CourseId,
                    Text = c.CourseName,
                    Selected = c.CourseId == model.CourseId
                }).ToList();

                ViewData["Courses"] = courses;
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Cập nhật avatar nếu có file mới được tải lên
            if (model.Avatar != null && model.Avatar.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Xóa avatar cũ nếu tồn tại
                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, user.Avatar);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Lưu avatar mới
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Avatar.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Avatar.CopyToAsync(fileStream);
                }
                user.Avatar = uniqueFileName;
            }

            // Cập nhật các thông tin khác
            user.FullName = model.FullName;
            user.Address = model.Address;
            user.DateOfBirth = model.DateOfBirth;
            user.CourseId = model.CourseId;

            // Cập nhật email nếu thay đổi
            if (user.Email != model.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    var courses = _context.Courses.Select(c => new SelectListItem
                    {
                        Value = c.CourseId,
                        Text = c.CourseName,
                        Selected = c.CourseId == model.CourseId
                    }).ToList();

                    ViewData["Courses"] = courses;
                    return View(model);
                }
                user.UserName = model.Email; // Đồng bộ UserName với Email
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["StatusMessage"] = "Cập nhật thông tin cá nhân thành công!";
                return RedirectToAction(nameof(Profile));
            }

            // Nếu cập nhật thất bại, hiển thị lỗi
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Tải lại danh sách khóa học để hiển thị lại view nếu có lỗi
            var coursesFallback = _context.Courses.Select(c => new SelectListItem
            {
                Value = c.CourseId,
                Text = c.CourseName,
                Selected = c.CourseId == model.CourseId
            }).ToList();

            ViewData["Courses"] = coursesFallback;
            return View(model);
        }

        // GET: Account/Index - Chỉ Admin mới có quyền truy cập
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 10; // Số người dùng trên mỗi trang
            var users = _userManager.Users.Include(u => u.Course).AsQueryable();
            var paginatedUsers = await PaginatedList<User>.CreateAsync(users, pageNumber, pageSize);
            return View(paginatedUsers);
        }

        // GET: Account/Details/id - Chỉ Admin mới có quyền xem chi tiết người dùng
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .Include(u => u.Course)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var viewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar,
                CourseName = user.Course?.CourseName,
                CourseId = user.CourseId,
                Roles = userRoles.ToList(),
                ExamResults = await _context.ExamResults
                    .Include(er => er.Exam)
                    .Where(er => er.UserId == id)
                    .OrderByDescending(er => er.SubmitTime)
                    .Take(5) // Chỉ lấy 5 kết quả thi gần nhất
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Account/Edit/id - Chỉ Admin mới có quyền sửa thông tin người dùng
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                CourseId = user.CourseId,
                CurrentAvatar = user.Avatar,
                UserRoles = userRoles.ToList(),
                AllRoles = allRoles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userRoles.Contains(r.Name)
                }).ToList()
            };

            var courses = _context.Courses.Select(c => new SelectListItem
            {
                Value = c.CourseId,
                Text = c.CourseName,
                Selected = c.CourseId == user.CourseId
            }).ToList();

            ViewData["Courses"] = courses;
            return View(model);
        }

        // POST: Account/Edit/id - Cập nhật thông tin người dùng
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                // Cập nhật avatar nếu có file mới được tải lên
                if (model.Avatar != null && model.Avatar.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Xóa avatar cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(user.Avatar))
                    {
                        string oldFilePath = Path.Combine(uploadsFolder, user.Avatar);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Lưu avatar mới
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Avatar.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Avatar.CopyToAsync(fileStream);
                    }
                    user.Avatar = uniqueFileName;
                }

                // Cập nhật các thông tin khác
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.DateOfBirth = model.DateOfBirth;
                user.CourseId = model.CourseId;

                // Cập nhật email nếu thay đổi
                if (user.Email != model.Email)
                {
                    var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                    if (!setEmailResult.Succeeded)
                    {
                        foreach (var error in setEmailResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                    user.UserName = model.Email; // Đồng bộ UserName với Email
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Xử lý cập nhật vai trò (roles)
                    if (model.SelectedRoles != null)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        // Xóa tất cả các roles hiện tại
                        await _userManager.RemoveFromRolesAsync(user, userRoles);

                        // Thêm các roles mới được chọn
                        await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                    }

                    TempData["StatusMessage"] = "Cập nhật thông tin người dùng thành công!";
                    return RedirectToAction(nameof(Index));
                }

                // Nếu cập nhật thất bại, hiển thị lỗi
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Nếu ModelState không hợp lệ, tải lại dữ liệu để hiển thị lại form
            var courses = _context.Courses.Select(c => new SelectListItem
            {
                Value = c.CourseId,
                Text = c.CourseName,
                Selected = c.CourseId == model.CourseId
            }).ToList();

            var allRoles = _roleManager.Roles.ToList();
            model.AllRoles = allRoles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name,
                Selected = model.SelectedRoles != null && model.SelectedRoles.Contains(r.Name)
            }).ToList();

            ViewData["Courses"] = courses;
            return View(model);
        }

        // GET: Account/Delete/id - Xác nhận xóa người dùng
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .Include(u => u.Course)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Không cho phép Admin xóa chính mình
            if (user.Id == _userManager.GetUserId(User))
            {
                TempData["ErrorMessage"] = "Bạn không thể xóa tài khoản của chính mình!";
                return RedirectToAction(nameof(ManageUsers));
            }

            return View(user);
        }

        // POST: Account/Delete/id - Thực hiện xóa người dùng
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Không cho phép Admin xóa chính mình
            if (user.Id == _userManager.GetUserId(User))
            {
                TempData["ErrorMessage"] = "Bạn không thể xóa tài khoản của chính mình!";
                return RedirectToAction(nameof(Index));
            }

            // Xóa avatar của người dùng nếu tồn tại
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
                string filePath = Path.Combine(uploadsFolder, user.Avatar);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // Kiểm tra và xóa liên kết với ExamResults trước khi xóa người dùng
            var examResults = await _context.ExamResults.Where(er => er.UserId == id).ToListAsync();
            if (examResults.Any())
            {
                _context.ExamResults.RemoveRange(examResults);
                await _context.SaveChangesAsync();
            }

            // Xóa người dùng
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["StatusMessage"] = "Xóa người dùng thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa người dùng!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Account/ManageUsers - Quản lý phân quyền người dùng
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUsers(string searchString, int pageNumber = 1)
        {
            int pageSize = 10; // Số người dùng trên mỗi trang
            var usersQuery = _userManager.Users.Include(u => u.Course).AsQueryable();

            // Tìm kiếm theo tên hoặc email nếu có searchString
            if (!string.IsNullOrEmpty(searchString))
            {
                usersQuery = usersQuery.Where(u =>
                    u.FullName.Contains(searchString) ||
                    u.Email.Contains(searchString));
            }

            var paginatedUsers = await PaginatedList<User>.CreateAsync(usersQuery, pageNumber, pageSize);

            // Lấy thông tin về role của mỗi user
            var viewModel = new List<UserRoleViewModel>();
            foreach (var user in paginatedUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                viewModel.Add(new UserRoleViewModel
                {
                    User = user,
                    Roles = roles.ToList()
                });
            }

            ViewData["SearchString"] = searchString;
            return View(new PaginatedList<UserRoleViewModel>(viewModel, paginatedUsers.Count, pageNumber, pageSize)
            {
                TotalPages = paginatedUsers.TotalPages,
                PageIndex = paginatedUsers.PageIndex
            });
        }

    }
}