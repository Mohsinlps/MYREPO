using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? CoursePrice { get; set; }
        public int? CourseWeeks { get; set; }
        public int CourseCategoryId { get; set; }
    }
}
