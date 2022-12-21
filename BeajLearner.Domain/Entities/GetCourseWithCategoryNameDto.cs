using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class GetCourseWithCategoryNameDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCategoryName { get; set; }
        public int CourseCategoryId { get; set; }
    }
}
