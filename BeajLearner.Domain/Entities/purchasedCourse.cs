using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class purchasedCourse
    {
        [Key]
        public int id { get; set; }
        public string customerId { get; set; }
        public int courseId { get; set; }
        public ICollection<Course> courses { get; set; }
    }
}
