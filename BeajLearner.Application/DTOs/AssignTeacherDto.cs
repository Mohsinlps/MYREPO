using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class AssignTeacherDto
    {
        public string teacherId { get; set; }
        public int[] courseId { get; set; }
    }
}
