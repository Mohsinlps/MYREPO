using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs.Account
{
    public class RegisterRequest
    {
        public string CellNumber { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }

        // public string UserName { get; set; }

        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }


    }
}
