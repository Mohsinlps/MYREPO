using Microsoft.AspNetCore.Mvc;


namespace BeajLearner.WebApp.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
           
          
            return View();
        }
        public IActionResult teacherDash()
        {
            return View();
        }
        

 
    }
}
