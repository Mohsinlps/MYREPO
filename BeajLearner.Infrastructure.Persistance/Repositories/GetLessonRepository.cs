﻿using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.DomainDtos;
using BeajLearner.Domain.Entities;
using BeajLearner.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance.Repositories
{
    public class GetLessonRepository: IGetLessonRepository
    {

        private IAsyncRepository<Lesson> _lessonRepo;
        public GetLessonRepository(IAsyncRepository<Lesson> lessonRepo)
        {
            _lessonRepo = lessonRepo;
        }



        //public IEnumerable<Lesson> GetLessonByCourseId(int id)
        //{
        //    IEnumerable<Lesson> obj = _lessonRepo.GetAll().Where(x => x.courseId == id).ToList();
        //    return obj;
        //}

        //public IEnumerable<Lesson> GetLessonByCourseIdWithMcqs(int id)
        //{
        //    IEnumerable<Lesson> obj = _lessonRepo.GetAll().Where(x => x.courseId == id).ToList();
        //    return obj;
        //}


        public IEnumerable<Lesson> GetLessonByCourseIdAndActivity(getMcqsDto dto)
        {
            if (dto.activity == "mcqs")
            {
                IEnumerable<Lesson> obj = _lessonRepo.getLessonWithMcqs(dto);
                if(obj!= null)
                {
                    return obj; 
                }
            }
            else
            {
                IEnumerable<Lesson> obj = _lessonRepo.GetAll().Where(x => x.courseId == dto.courseId && x.activity == dto.activity).ToList();
                return obj;
            }
            return null;
        }

        public IEnumerable<Lesson> GetLessonByCourseId(int id)
        {
           
             
          
                IEnumerable<Lesson> obj = _lessonRepo.GetLessonByCourseIdwithMcqs( id);
          if(obj != null)
            {
                return obj;
            }
            else { return null; }
        }


    }
}
