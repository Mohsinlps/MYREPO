using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class CourseCategoryDto
    {
        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
        public IFormFile[] image { get; set; }
    }
}
