using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class getAllCategoriesDto
    {
        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
        public string image { get; set; }
    }
}
