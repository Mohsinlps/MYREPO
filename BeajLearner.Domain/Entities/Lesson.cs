using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    //public enum  type
    //{
    //week,
    //day
    //}
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }
        public string lessonType { get; set; }
        public int? dayNumber { get; set; }
        public string activity { get; set; }
        public string activityAlias { get; set; }
        public int? weekNumber { get; set; }
        public string[] videos { get; set; }
        public string[] Audios { get; set; }
        public string[] image { get; set; }
        public string text { get; set; }
        public ICollection< mcqQuestions> mcqquestion {get;set;}
        public int courseId { get; set; }
        public Course Course { get; set; }
    }
}
