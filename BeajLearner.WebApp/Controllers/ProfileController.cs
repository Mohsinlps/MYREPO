using BeajLearner.WebApp.Models.Profile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Text;

namespace BeajLearner.WebApp.Controllers
{

    public class ProfileController : Controller
    {
        string BaseURL = "https://localhost:7223/api/v1/";
        private readonly IHttpClientFactory _clientFactory;

        public ProfileController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("UserRole");
            string jwt = HttpContext.Session.GetString("jwt");
            if (role == "" && jwt == "")
            {

                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            string errorString;
            string jwt = HttpContext.Session.GetString("jwt");
            UserProfile user;
            var url = BaseURL + "User?id=" + id;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt);
            HttpResponseMessage respone = await client.SendAsync(request);
            if (respone.IsSuccessStatusCode)
            {
                user = await respone.Content.ReadFromJsonAsync<UserProfile>();
                errorString = null;
                return Json(user);
            }
            else
            {
                errorString = $"Error: {respone.ReasonPhrase}";
                return Json(errorString);

            }
        }

        public async Task<IActionResult> UpdateUser(Guid id, string firstName, string lastName, string role, string email)
        {
            if (ModelState.IsValid)
            {
                string jwt = HttpContext.Session.GetString("jwt");
                var url = BaseURL + "User/UpdateUser";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Put);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + jwt);
                var body = new
                {
                    id,
                    firstName,
                    lastName,
                    role,
                    email
                };
                var bodyy = JsonConvert.SerializeObject(body);
                request.AddBody(bodyy, "application/json");
                RestResponse response = await client.ExecuteAsync(request);
                var output = response.Content;
                dynamic val = JsonConvert.DeserializeObject(output);
                if (val.succeeded == "true")
                {
                    return Json(val);

                }


            }
            return Json("Error");
        }

        public async Task<IActionResult> UpdateUserPassword(Guid id, string newPassword, string confirmedPassword)
        {
            return null;
        }
    }
}
