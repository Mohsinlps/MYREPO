using BeajLearner.Domain.Entities;
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
        public string[] Audios { get; set; }
        public string[] image { get; set; }
        public string lessonType { get; set; }
        public int? dayNumber { get; set; }
        public string activity { get; set; }
        public string activityAlias { get; set; }
        public ICollection<mcqQuestions> mcqquestion { get; set; }
        public int courseId { get; set; }
        public string CourseName { get; set; }
        public int? weekNumber { get; set; }
        public string[] videos { get; set; }
        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
    }
}
