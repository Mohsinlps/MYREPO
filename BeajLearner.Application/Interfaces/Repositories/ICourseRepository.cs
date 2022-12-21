using BeajLearner.Application.DTOs;
using BeajLearner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        public void AddCourse(CourseDto dto);

        public List<CourseDto> GetAllCourse();
        public List<CourseDto> GetAllCourseByCategoryId(int categoryid);

        public CourseDto GetCourseById(int id);

        public void UpdateCourse(CourseDto dto);

        public void Delete(int id);
        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategory();
        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategoryById(int id);

    }
}
