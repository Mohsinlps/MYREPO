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
    public class GetCoursesController : ControllerBase
    {

        private IMapper _mapper;
        private IGetCourseRepository _courseRepo;
        public GetCoursesController(IGetCourseRepository courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        [HttpGet("GetCourseByCategoryId")]
        public IEnumerable<CourseDto> GetCoursesbyCategoryId(int id)
        {
            List<CourseDto> dto = new List<CourseDto>();
            try
            {
                IEnumerable<Course> model = _courseRepo.GetCourseByCategoryId(id);
                if(model!= null)
                {
                    dto = _mapper.Map<List<CourseDto>>(model);
                    return dto;
                }
                else
                {
                    return dto;
                }
            }
            catch(Exception ex)
            {
               
            }
           
            return dto;

        }
    }
}
