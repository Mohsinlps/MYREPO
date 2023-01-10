using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class SpeakQuestion
    {
        public int id { get; set; }
        public int lessonId { get; set; }
        public string question { get; set; }

        public ICollection<SpeakAnswer> speakAnswer { get; set; }
    }
}
