

using BeajLearner.Application.DTOs.Account;
using BeajLearner.Application.DTOs.User;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeajLearner.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int EmployeeNo { get; set; }
        //public Guid CityId { get; set; }

        //---------------For Profile-----------------
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        //--------------------------------------------
        public string CellNumber { get; set; }

        public bool? IsDeleted { get; set; }
        [Column(TypeName = "jsonb")]
        public UserDocumentsDTO Documents { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}
