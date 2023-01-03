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
    public class CourseCategoryController : ControllerBase
    {

        private ICourseCategoryRepository _CourseCategoryService { get; set; }

        public CourseCategoryController(ICourseCategoryRepository service)
        {
            _CourseCategoryService = service;
        }

        [HttpPost]
        [Route("AddCourseCategory")]
        public async Task<IActionResult> Post([FromForm] CourseCategoryDto categorydto)
        {
            string host = HttpContext.Request.Host.Value;
            host = "https://" + host + "//";

            categorydto.savingPort = host;
            categorydto =   _CourseCategoryService.AddCourseCategory(categorydto);
            return Ok(new Response<CourseCategoryDto>(categorydto, "Created Sucessfully"));
        }

        




        [HttpGet]
        [Route("GetAllCourseCategories")]
        public List<getAllCategoriesDto> GetAll()
        {
            List<getAllCategoriesDto> dto = new List<getAllCategoriesDto>();
            dto = _CourseCategoryService.GetAllCourseCategories();
            return dto;
        }


        //[HttpGet]
        //[Route("GetAllRelated")]
        //public IEnumerable<CourseCategory> GetAllRelated(int id)
        //{
        //    IEnumerable<CourseCategory> dto = _CourseCategoryService.GetAllRelated( id);
          
        //    return dto;
        //}

        [HttpGet]
        [Route("GetCourseCategoryById")]
        public CourseCategoryDto GetById(int id)
        {
            CourseCategoryDto dto = _CourseCategoryService.GetCourseCategoryById(id);
            return dto;
        }

        [HttpPost]
        [Route("UpdateCourseCategory")]
        public async Task<IActionResult> Put([FromForm] CourseCategoryDto dto)
        {
            _CourseCategoryService.UpdateCourseCategory(dto);
            return Ok(new Response<int>(dto.CourseCategoryId, "Updated Successfully"));
        }
        [HttpPost]
        [Route("DeleteCourseCategory")]
        public async Task<IActionResult> Delete(int id)
        {
            _CourseCategoryService.Delete(id);
            return Ok(new Response<int>(id, "Deleted Successfully"));
        }



    }
}
