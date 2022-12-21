using BeajLearner.WebApp.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;

namespace BeajLearner.WebApp.Controllers.Users
{
   

    public class UserController : Controller
    {

        string BaseURL = "https://localhost:7223/api/v1/User/";
        private readonly IHttpClientFactory _clientFactory;

        public UserController(IHttpClientFactory clientFactory)
        {
        
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("UserRole");
            string jwt = HttpContext.Session.GetString("jwt");
            if(role == "" && jwt == "")
            {

                return RedirectToAction("Index", "Login");
            }
            else { return View(); }
            
        }
        public async Task<IActionResult> GetUser()    
        {
            string errorString;
            string jwt = HttpContext.Session.GetString("jwt");
            User user; 
            var url = BaseURL + "AllUsers";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+jwt);
            HttpResponseMessage respone = await client.SendAsync(request);
            if (respone.IsSuccessStatusCode)
            {
                user = await respone.Content.ReadFromJsonAsync<User>();
                errorString = null;
                return Json(user);
            }
            else
            {
                errorString = $"Error: {respone.ReasonPhrase}";
                return Json(errorString);

            }
        }
    }
}
