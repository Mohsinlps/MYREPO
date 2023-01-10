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
        public void addCourseWeekInfo(courseWeekInfoDto dto);
        public IEnumerable<CourseWeek> GetWeeksByCourseId(int id);
        public CourseWeek GetWeeksByCourseIdAndWeekNumber(int id, int weekNumber);
        public void AddMcqs(List< mcqsDto> dto);
        public void updateMcqs(List<mcqsDto> dto);
        public void deleteMcqs(int id);
        public IEnumerable<GetLessonWithCourseAndCategoryDto> GetAllLessonWithCatAndCourse();

        public IEnumerable<CourseCategory> GetAllLesson(int courseCategoryId);


        public GetLessonsDto GetLessonById(int id);
      


        public  Task<LessonDto> UpdateLesson(LessonDto input);


        public void Delete(int id);
        public void addActivityAlias(string alias);
        public IEnumerable<ActivityAlias> getActivityAlias();
        public void deleteActivityAlias(int id);

    }
}
