using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using DisCourse.Models;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Tạo vai trò nếu chưa có
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Tạo tài khoản Admin mặc định
        var adminEmail = "admin@example.com";
        var adminPassword = "Admin123@";
        IdentityUser admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        // Thêm khóa học mẫu nếu chưa có
        if (!context.Courses.Any())
        {
            var courses = new[]
            {
                new Course
                {
                    Name = "Không chủ đề",
                    Description = "Chủ đề tự do, có thể dành cho thảo luận về trong khóa học hoặc ngoài khóa học :)",
                    CreatedAt = DateTime.UtcNow,
                    OwnerID = admin.Id
                }
            };

            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();
        }
    }
}
