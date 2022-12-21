using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.Entities;
using BeajLearner.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance.Repositories
{
    public class GetCourseRepository:IGetCourseRepository
    {

        private IAsyncRepository<Course> _courseRepo;
        public GetCourseRepository(IAsyncRepository<Course> courseRepo)
        {
            _courseRepo = courseRepo;
        }



        public IEnumerable<Course> GetCourseByCategoryId(int id)
        {
            IEnumerable<Course> obj = _courseRepo.GetAll().Where(x => x.CourseCategoryId == id).ToList();
            return obj;
        }
    }
}
