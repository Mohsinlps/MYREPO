using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class DocumentFiles
    {
        public int id { get; set; }
        public int lessonId { get; set; }
        public string language { get; set; }
        public string video { get; set; }
        public string audio { get; set; }
        public string image { get; set; }
        public string mediaType { get; set; }
       // public Lesson lesson { get; set; }
    }
       
    
}
