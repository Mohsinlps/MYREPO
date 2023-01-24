using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class SpeakActivityQuestions
    {
        public int id { get; set; }
        public int lessonId { get; set; }
        public string question { get; set; }
        public string[] answer { get; set; }
        //public string audio { get; set; }
        public string mediaFile { get; set; }
        public Lesson lesson { get; set; }
    }
}
