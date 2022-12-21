using BeajLearner.Application.DTOs.Account;
using BeajLearner.WebApp.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using BeajLearner.WebApp.Sessions;
namespace BeajLearner.WebApp.Controllers.Auth
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class LoginController : Controller
    {
        
          string Baseurl = "https://localhost:7223/api/v1/Account/";
       // string Baseurl = "https://lpsapps.com:6043/api/v1/Account/";
       SessionsClass sessionObj=new SessionsClass();
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public void SaveSession(SessionsClass obj)
        //{
        //    HttpContext.Session.SetString("UserRole", "");
        //    HttpContext.Session.SetString("jwt", "");
        //    HttpContext.Session.SetString("FullName", "");
        //    HttpContext.Session.SetString("Email", "");



        //    HttpContext.Session.SetString("UserRole", obj.UserRole);
        //    HttpContext.Session.SetString("UserId", obj.UserId);
        //    HttpContext.Session.SetString("jwt", obj.jwt);
        //    //HttpContext.Session.SetString("FullName", obj.);
        //    HttpContext.Session.SetString("Email", obj.UserEmail);
        //}

        //        [HttpPost]
        //        public async Task<IActionResult> logIn([FromBody] UserLogin useLogin)
        //        {


        //            if (ModelState.IsValid)
        //            {
        //                sessionObj.EmptySessions();

        //                HttpClient clientt = new HttpClient();
        //                AuthenticationRequest obj = new AuthenticationRequest { Email = useLogin.Email, Password = useLogin.Password };
        //                clientt = new HttpClient { BaseAddress = new Uri(Baseurl) };

        //                var response = await clientt.PostAsJsonAsync("authenticate", obj);
        //                var responseString = await response.Content.ReadAsStringAsync();
        //                dynamic val = JsonConvert.DeserializeObject(responseString);

        //                var authorizing = val.succeeded;
        //                if (authorizing == "true")
        //                {

        //                    HttpContext.Session.SetString("UserRole", "");
        //                    HttpContext.Session.SetString("jwt", "");
        //                    HttpContext.Session.SetString("FullName", "");
        //                    HttpContext.Session.SetString("Email", "");

        //                    string UserRole = val.data.role;
        //                    string UserId = val.data.id;
        //                    string jwt = Convert.ToString(val.data.jwToken);
        //                    string UserEmail = Convert.ToString(val.data.email);
        //                    string FullName = Convert.ToString(val.data.firstName) + "  " + Convert.ToString(val.data.lastName);

        //                    HttpContext.Session.SetString("UserRole", UserRole);
        //                    HttpContext.Session.SetString("UserId", UserId);
        //                    HttpContext.Session.SetString("jwt", jwt);
        //                    HttpContext.Session.SetString("FullName", FullName);
        //                    HttpContext.Session.SetString("Email", UserEmail);


        //                    return Json(new { JwtToken = jwt, Role = UserRole });

        //                }


        //            }
        //            return Json(0);
        //        }

        //        public IActionResult LogOut()
        //        {
        //            HttpContext.Session.SetString("UserRole", "");
        //            HttpContext.Session.SetString("jwt", "");
        //            HttpContext.Session.SetString("FullName", "");
        //            return RedirectToAction("Index", "Login");
        //        }

    }


}
