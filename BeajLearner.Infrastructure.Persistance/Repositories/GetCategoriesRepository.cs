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
    public class GetCategoriesRepository: IGetCategoriesRepository
    {
        private IAsyncRepository<CourseCategory> _categoryRepo;
        public GetCategoriesRepository(IAsyncRepository<CourseCategory> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

      
        public IEnumerable<CourseCategory> GetAllCategories()
        {

            IEnumerable<CourseCategory> obj = _categoryRepo.GetAll().ToList();
            return obj;
        }
    }
}
