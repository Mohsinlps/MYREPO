using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApp.Controllers
{
    public class TeacherHireWebController : Controller
    {
        public IActionResult hireTeacher()
        {
            return View();
        }

        public IActionResult AssignCourses()
        {
            return View();
        }
    }
}
