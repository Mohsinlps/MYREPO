﻿namespace BeajLearner.WebApp.Models.Auth
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsVerified { get; set; }


    }
}
