using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class updateSpeakActivityQuestionDto
    {
        public int lessonId { get; set; }
        public string question { get; set; }
        public string[] answer { get; set; }
        public IFormFile audio { get; set; }
        public string savingPort { get; set; }
        public int savedSpeakId { get; set; }
        public string oldMediaFilePath { get; set; }
        
    }
}
