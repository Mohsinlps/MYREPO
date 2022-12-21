using BeajLearner.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int EmployeeNo { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        public Guid CityId { get; set; }
        public string City { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public UserDocumentsDTO UserDocument { get; set; }
    }
}
