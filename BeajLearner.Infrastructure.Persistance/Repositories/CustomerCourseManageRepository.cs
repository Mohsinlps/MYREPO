using BeajLearner.Application.DTOs;
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
    public class CustomerCourseManageRepository: ICustomerCourseManageRepository
    {
        private IAsyncRepository<purchasedCourse> _repo;
        public CustomerCourseManageRepository(IAsyncRepository<purchasedCourse> repo)
        {
            _repo = repo;
        }
        public void savePurchasedCourse(purchaseCourseDto dto)
        {
        
        }
    }
}
