using BeajLearner.Application.DTOs;
using BeajLearner.Domain.DomainDtos;
using BeajLearner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        public  Task testMethod(int id);
        public  Task<LessonDto> AddLesson( LessonDto input);
        public void AddMcqs(List< mcqsDto> dto);
        public void updateMcqs(List<mcqsDto> dto);
        public void deleteMcqs(int id);
        public IEnumerable<GetLessonWithCourseAndCategoryDto> GetAllLessonWithCatAndCourse();



        public IEnumerable<CourseCategory> GetAllLesson(int courseCategoryId);


        public LessonDto GetLessonById(int id);



        public  Task<LessonDto> UpdateLesson(LessonDto input);


        public void Delete(int id);
       
    }
}
