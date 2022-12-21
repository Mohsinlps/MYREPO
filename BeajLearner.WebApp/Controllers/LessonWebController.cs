using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApp.Controllers
{
    public class LessonWebController : Controller
    {
        public IActionResult AddLesson()
        {
            return View();
        }

       
    }
}

