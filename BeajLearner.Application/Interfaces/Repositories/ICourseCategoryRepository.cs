using BeajLearner.Application.DTOs;
using BeajLearner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface ICourseCategoryRepository
    {
        public CourseCategoryDto AddCourseCategory(CourseCategoryDto dto);
        public List<getAllCategoriesDto> GetAllCourseCategories();
        public CourseCategoryDto GetCourseCategoryById(int id);
        public void UpdateCourseCategory(CourseCategoryDto dto);
        public void Delete(int id);
      //  public IEnumerable<CourseCategory> GetAllRelated(int id);


    }
}
