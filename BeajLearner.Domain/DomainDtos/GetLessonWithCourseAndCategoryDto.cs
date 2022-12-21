using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.DomainDtos
{
    public class GetLessonWithCourseAndCategoryDto
    {
        public int LessonId { get; set; }
        public string week { get; set; }
        public string text { get; set; }
        public string[] Videos { get; set; }
        public string[] Audios { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
    }
}
