using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class LessonDto
    {


        public int LessonId { get; set; }
        public string lessonType { get; set; }
        public int? dayNumber { get; set; }
        public string activity { get; set; }
        public int? weekNumber { get; set; }
        public IFormFile[] videos { get; set; }
        public IFormFile[] Audios { get; set; }
        public IFormFile[] image { get; set; }
        public string text { get; set; }
       public string mcqs { get; set; }
        public int courseId { get; set; }
        public string savingPort { get; set; }


      
















    }
}
