using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class courseWeekInfoDto
    {
        public int courseId { get; set; }
        public int weekNumber { get; set; }
        public string description { get; set; }
        public string savingPort { get; set; }
        public IFormFile image { get; set; }
    }
}
