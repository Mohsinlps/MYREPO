using AutoMapper;
using BeajLearner.Application.DTOs;

using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Application.Wrappers;
using BeajLearner.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseRepository _CourseService { get; set; }

        public CourseController(ICourseRepository service)
        {
            _CourseService = service;
        }

        [HttpPost]
        [Route("AddCourse")]
        public async Task<IActionResult> Post([FromForm] CourseDto dto)
        {
            _CourseService.AddCourse(dto);
            return Ok(new Response<int>(dto.CourseId, "Created Sucessfully"));
        }

        [HttpGet]
        [Route("GetAllCourse")]
        public List<CourseDto> GetAll()
        {
            List<CourseDto> dto = new List<CourseDto>();
            dto = _CourseService.GetAllCourse();
            return dto;
        }

        [HttpGet]
        [Route("GetAllCourseByUserId")]
        public IEnumerable< CourseDto> getAllByUserId(string Id)
        {
         
            IEnumerable<CourseDto> dto = _CourseService.GetAllCoursesForUser(Id);
            return dto;
        }

        [HttpGet]
        [Route("GetAllCourseWithCategoryModel")]
        public IQueryable<GetCourseWithCategoryNameDto> GetAllModel( )
        {

            IQueryable<GetCourseWithCategoryNameDto> model = _CourseService.GetCourseWithCategory();
            return model;
        }


        [HttpPost]
        [Route("GetAllCourseWithCategoryModelById")]
        public IQueryable<GetCourseWithCategoryNameDto> GetAllModelbyId(int id)
        {

            IQueryable<GetCourseWithCategoryNameDto> model = _CourseService.GetCourseWithCategoryById(id);
            return model;
        }










        [HttpGet]
        [Route("GetAllCourseByCategoryId")]
        public List<CourseDto> GetAllByCategoryId(int categoryId)
        {
            List<CourseDto> dto = new List<CourseDto>();
            dto = _CourseService.GetAllCourseByCategoryId(categoryId);
            return dto;
        }



        [HttpPost]
        [Route("GetCourseById")]
        public CourseDto GetById(int id)
        {
            CourseDto dto = _CourseService.GetCourseById(id);
            return dto;
        }

        [HttpPost]
        [Route("UpdateCourse")]
        public  IActionResult Put([FromForm] CourseDto dto)
        {
            _CourseService.UpdateCourse(dto);
            return Ok(new Response<int>(dto.CourseId, "Updated Successfully"));
        }

        [HttpPost]
        [Route("DeleteCourse")]
        public  IActionResult Delete(int id)
        {
            _CourseService.Delete(id);
            return Ok(new Response<int>(id, "Deleted Successfully"));
        }


    }
}
