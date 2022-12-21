using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.DomainDtos
{
    public class getMcqsDto
    {
        public int LessonId { get; set; }
        public string week { get; set; }
        public string text { get; set; }
        public string[] Videos { get; set; }
        public string[] Audios { get; set; }
        public string[] image { get; set; }
        public string lessonType { get; set; }
        public int? dayNumber { get; set; }
        public int? weekNumber { get; set; }
        public string activity { get; set; }
        public string mcqs { get; set; }
        public int courseId { get; set; }
    }
}
