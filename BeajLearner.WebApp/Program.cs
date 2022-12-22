using BeajLearner.WebApp.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Environment.SetEnvironmentVariable("ASPNETCORE_APIURL",builder.Configuration.GetSection("URLs").GetSection("APIUrl").Value);
// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(Options => 
{

      Options.Cookie.IsEssential = true;
    Options.IdleTimeout = TimeSpan.FromMinutes(20);
});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
//    (Options => { Options.LoginPath = "/Login/Index"; });

//builder.Services.AddSession(options =>
//     {
//         options.IdleTimeout = TimeSpan.FromMinutes(30);
//     });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
// pattern: "{controller=CourseWeb}/{action=GetAllCourses}/{id?}");
//pattern: "{controller=CourseWeb}/{action=AddCourse}/{id?}");
//pattern: "{controller=LessonWeb}/{action=AddLesson}/{id?}");
//pattern: "{controller=Login}/{action=Index}/{id?}");
//pattern: "{controller=CourseCategoryWeb}/{action=AddCourseCategory}/{id?}");
//    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
//pattern: "{controller=Stepper}/{action=Stepper}/{id?}");
//pattern: "{controller=Stepper}/{action=StepperTeacher}/{id?}");
//pattern: "{controller=TeacherHireWeb}/{action=AssignCourses}/{id?}");
pattern: "{controller=LessonWeb}/{action=getLessons}/{id?}");
app.Run();
