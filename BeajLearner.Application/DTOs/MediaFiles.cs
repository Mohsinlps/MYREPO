using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class MediaFiles
    {
        public int id { get; set; }
        public int lessonId { get; set; }
        public string language { get; set; }
        public IFormFile video { get; set; }
        public IFormFile audio { get; set; }
        public string mediaType { get; set; }
        public string savingPort { get; set; }

    }
}
