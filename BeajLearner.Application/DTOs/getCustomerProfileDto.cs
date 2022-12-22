using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class getCustomerProfileDto
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
    }
}
