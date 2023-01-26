using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class updateSpeakActivityQuestionWithoutAudioDto
    {
        public int lessonId { get; set; }
        public string question { get; set; }
        public string[] answer { get; set; }
        public string audio { get; set; }
        
        public int savedSpeakId { get; set; }
     
    }
}
