using BeajLearner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class GetLessonsDto
    {
        public int LessonId { get; set; }
        public string week { get; set; }
        public string text { get; set; }
       
        public string[] Audios { get; set; }
        public string[] image { get; set; }
        public string lessonType { get; set; }
        public int? dayNumber { get; set; }
        public string activity { get; set; }
        public ICollection<mcqQuestions> mcqquestion { get; set; }
        public int courseId { get; set; }


      
        public int? weekNumber { get; set; }
        public string[] videos { get; set; }
      
     
       
    }
}
