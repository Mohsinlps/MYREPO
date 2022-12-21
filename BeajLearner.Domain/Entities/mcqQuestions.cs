using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class mcqQuestions
    {
        [Key]
        public int Id { get; set; }
        
        public string question { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string correctAnswer { get; set; }
        public int lessonId { get; set; }
      //  public Lesson lesson { get; set; }
    }
}
