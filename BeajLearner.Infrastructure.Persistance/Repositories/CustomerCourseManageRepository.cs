using AutoMapper;
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
        private IMapper _mapper;
        public CustomerCourseManageRepository(IAsyncRepository<purchasedCourse> repo,IMapper mapper)
        {
            _mapper=mapper;
            _repo = repo;
        }
        public purchaseCourseDto savePurchasedCourse(purchaseCourseDto dto)
        {
            purchasedCourse model=_mapper.Map<purchasedCourse>(dto);
            try
            {
                model = _repo.Add(model);

                return dto;
            }
            catch(Exception ex)
            {
                return dto;
            }

        }
    }
}
