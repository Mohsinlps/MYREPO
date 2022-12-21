using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApp.Controllers
{
    public class TestController : Controller
    {
        string BaseURL = "https://localhost:7223/api/v1/";
     

        public TestController()
        {
          
        }
        public IActionResult Index()
        {
           
            
            return View();
        }

      
    }
}
