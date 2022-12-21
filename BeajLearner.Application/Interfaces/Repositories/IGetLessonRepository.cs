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
    public interface IGetLessonRepository
    {
        //public IEnumerable<Lesson> GetLessonByCourseId(int id);
        public IEnumerable<Lesson> GetLessonByCourseIdAndActivity(getMcqsDto dto);
        public IEnumerable<Lesson> GetLessonByCourseId(int id);
    }
}
