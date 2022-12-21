using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs.Account
{
    public class GenerateCustomerProfileDto
    {
        public string UserName { get; set; }
        public String DOB { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string Name { get; set; }
       // public string LastName { get; set; }
    }
}
