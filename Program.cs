﻿using DisCourse.Models;
using DisCourse.Repository;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Repository
builder.Services.AddScoped<ICourseRepository, EFCourseRepository>(); // Đăng ký CourseRepository
builder.Services.AddScoped<IPostRepository, EFPostRepository>();
builder.Services.AddScoped<ICommentRepository, EFCommentRepository>();


// Đăng ký Controllers & Views
builder.Services.AddControllersWithViews();

// Đăng ký Authorization (nếu cần)
builder.Services.AddAuthorization();

// Add this before building the app
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB limit
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization(); // Chỉ dùng nếu có Authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
