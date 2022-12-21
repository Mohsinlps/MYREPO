using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class CourseCategory
    {

        [Key]
        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
        public string image { get; set; }
        public ICollection<Course> Course { get; set; }
    }
}
