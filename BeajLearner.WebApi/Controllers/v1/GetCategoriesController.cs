using AutoMapper;
using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCategoriesController : ControllerBase
    {
        private IGetCategoriesRepository _categoryRepo;
        private IMapper _mapper;
        public GetCategoriesController(IGetCategoriesRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

      
        //[HttpGet("GetAllCategories")]
        //public IEnumerable<CourseCategoryDto> getCategories()
        //{
        //    List<CourseCategoryDto> dto=new List<CourseCategoryDto>();
        //    try
        //    {
        //        IEnumerable<CourseCategory> obj = _categoryRepo.GetAllCategories();
        //        if(obj!=null)
        //        {
        //           dto = _mapper.Map<List<CourseCategoryDto>>(obj);
        //            return dto;
        //        }
        //        else
        //        {
        //            return dto;
        //        }
               
        //    }
        //    catch(Exception ex)
        //    {

        //    }
           
        //    return dto;

        //}

    }
}
