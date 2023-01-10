using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class SpeakAnswer
    {
        public int id { get; set; }
        public string answer { get; set; }
        public SpeakQuestion speakQuestion { get; set; }
       
    }
}
