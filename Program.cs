using DisCourse.Models;
using DisCourse.Repository;
using DisCourseW;
using DisCourseW.Repository;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
}).AddDefaultTokenProviders()
  .AddDefaultUI()
  .AddEntityFrameworkStores<ApplicationDbContext>();

// Đăng ký Razor Pages
builder.Services.AddRazorPages();

// Đăng ký Repository
builder.Services.AddScoped<ICourseRepository, EFCourseRepository>();
builder.Services.AddScoped<IPostRepository, EFPostRepository>();
builder.Services.AddScoped<ICommentRepository, EFCommentRepository>();
builder.Services.AddScoped<IUserCourseRepository, EFUserCourseRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IUserProfilePictureRepository, EFUserProfilePictureRepository>();
builder.Services.AddScoped<ILikePostRepository, EFLikePostRepository>();

// Cấu hình giới hạn upload file
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB
});

// Build ứng dụng
var app = builder.Build();

// Seed dữ liệu (nếu cần)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.Initialize(services);
        Console.WriteLine("Seed dữ liệu thành công!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi khi seed dữ liệu: {ex.Message}");
        Console.WriteLine(ex.StackTrace); // In chi tiết lỗi để debug
        throw;
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Cấu hình Routing
app.UseEndpoints(endpoints =>
{
    // Route cho Area "Teacher"
    endpoints.MapAreaControllerRoute(
        name: "teacher",
        areaName: "Teacher",
        pattern: "Teacher/{controller=Course}/{action=Index}/{id?}");

    // Route mặc định cho site chính
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Razor Pages (dành cho Identity UI)
    endpoints.MapRazorPages();
});

// Chạy ứng dụng
app.Run();
