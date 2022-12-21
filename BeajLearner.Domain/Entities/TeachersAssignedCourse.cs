using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class TeachersAssignedCourse
    {
        [Key]
        public int Id { get; set; }
        public string teacherId { get; set; }
        public int courseId { get; set; }
    }
}
