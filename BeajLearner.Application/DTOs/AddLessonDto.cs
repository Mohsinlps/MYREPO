using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class AddLessonDto
    {
        public int LessonId { get; set; }
        public string week { get; set; }
        public string text { get; set; }
        public IFormFile[] Videos { get; set; }
        public IFormFile[] Audios { get; set; }
        public int CourseId { get; set; }
        public int CourseCategoryId { get; set; }
    }
}
