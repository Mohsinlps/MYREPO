using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? CoursePrice { get; set; }
        public int? CourseWeeks { get; set; }
        public int CourseCategoryId { get; set; }
        public bool subscribed { get; set; }
        public string status { get; set; }
        public CourseCategory CourseCategory { get; set; }
      
        public ICollection<Lesson> lessons { get; set; }
    }
}
