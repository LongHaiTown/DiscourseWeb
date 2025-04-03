using DisCourse.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class UserCourseViewModel
{
    public Course Course { get; set; }
    public List<IdentityUser> Users { get; set; }
}