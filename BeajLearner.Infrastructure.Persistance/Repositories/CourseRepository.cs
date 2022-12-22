﻿using AutoMapper;
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
    public class CourseRepository:ICourseRepository
    {
        private IMapper _mapper;
        private IAsyncRepository<Course> _repo;
        private IAsyncRepository<purchasedCourse> _repoPurchasedCourse;
        public CourseRepository(IMapper mapper, IAsyncRepository<Course> repo, IAsyncRepository<purchasedCourse> repoPurchasedCourse)
        {
            _mapper = mapper;
            _repo = repo;
            _repoPurchasedCourse = repoPurchasedCourse;
        }
        public void AddCourse(CourseDto dto)
        {
            Course coursecat = new Course();
            coursecat = _mapper.Map<Course>(dto);
            _repo.Add(coursecat);
        }
        public List<CourseDto> GetAllCourse()
        {
            IEnumerable<Course> course = _repo.GetAll();

            List<CourseDto> dto = _mapper.Map<List<CourseDto>>(course);
            return dto;
        }


        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategory()
        {
      IQueryable<    GetCourseWithCategoryNameDto> dto=  _repo.GetCourseWithCategory();
            return dto;
        }

        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategoryById(int id)
        {
            IQueryable<GetCourseWithCategoryNameDto> dto = _repo.GetCourseWithCategoryById(id);
            return dto;
        }

      




        public List<CourseDto> GetAllCourseByCategoryId(int categoryid)
        {
            IEnumerable<Course> course = _repo.GetAll().Where(x=>x.CourseCategoryId==categoryid);

            List<CourseDto> dto = _mapper.Map<List<CourseDto>>(course);
            return dto;
        }
        //*********************************************************************************

        public IEnumerable< CourseDto> GetAllCoursesForUser(string CustomerId)
        {

        IEnumerable<purchaseCourseDto> purchasedCourses =_mapper.Map<IEnumerable< purchaseCourseDto> >(_repoPurchasedCourse.GetAll());
            //-------------------------------
            IEnumerable<Course> allCourses = _repo.GetAll();

            //----------------------------------------
      

         var test= allCourses.Where(ac => purchasedCourses.Select(pc => pc.courseId).Contains(ac.CourseId));
            foreach(var i in test)
            {
                allCourses.FirstOrDefault(x=> x.CourseId==i.CourseId).CoursePrice=0;
            }

            IEnumerable<CourseDto> coursedto = _mapper.Map<IEnumerable< CourseDto>>(allCourses);
          
            return coursedto;
        }


        public CourseDto GetCourseById(int id)
        {
            CourseDto dto = new CourseDto();
            Course course = _repo.GetById(id);
            dto = _mapper.Map<CourseDto>(course);
            return dto;
        }

        public void UpdateCourse(CourseDto dto)
        {
            Course course = _mapper.Map<Course>(dto);
            _repo.Update(course);
        }
        public void Delete(int id)
        {
            _repo.Delete(id);

        }
    }
}
