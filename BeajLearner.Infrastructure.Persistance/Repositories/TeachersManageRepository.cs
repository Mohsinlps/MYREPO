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
    public class TeachersManageRepository:ITeachersManageRepository
    {
        private IMapper _mapper;
        private IAsyncRepository<TeachersAssignedCourse> _repo;
        public TeachersManageRepository(IAsyncRepository<TeachersAssignedCourse> repo)
        {
           _repo = repo;
        }
        public void AssignCourse(AssignTeacherDto dto)
        {
            TeachersAssignedCourse model=new TeachersAssignedCourse();
            model.teacherId = dto.teacherId;
           
           // model=_mapper.Map<TeachersAssignedCourse>(dto);
            foreach(int i in dto.courseId)
            {
               
                try
                {
                    model.Id = 0;
                    model.courseId = i;
                    _repo.Add(model);
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}
