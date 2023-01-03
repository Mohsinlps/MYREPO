using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class CourseWeek
    {
        public int id { get; set; }
        public int weekNumber { get; set; }
        public string image { get; set; }
        public string description { get; set; }
       
        public int courseId { get; set; }
    

    }
}
