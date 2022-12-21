using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApp.Controllers
{
    public class CourseWebController : Controller
    {
     
        public CourseWebController()
        {
         
        }
        public IActionResult AddCourse()
        {
            return View();
        }

    
    }
}
