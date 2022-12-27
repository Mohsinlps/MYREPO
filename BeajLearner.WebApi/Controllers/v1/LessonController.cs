using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Application.Wrappers;
using BeajLearner.Domain.DomainDtos;
using BeajLearner.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private IWebHostEnvironment Environment;
        private ILessonRepository _lessonRepository;
       
        public LessonController(ILessonRepository lessonRepository, IWebHostEnvironment env)
        {
            _lessonRepository = lessonRepository;
            Environment = env;
        }

        //********************************************************

        [HttpGet]
        [Route("GetLessonWithCourseAndCategory")]
        public IEnumerable< GetLessonWithCourseAndCategoryDto> GetLessonWithCourseAndCategory()
        {
       IEnumerable<GetLessonWithCourseAndCategoryDto> dto=     _lessonRepository.GetAllLessonWithCatAndCourse();
            return dto;
        }


        //*******************************************************

        [HttpPost]
        [Route("AddLesson")]
        public async Task<IActionResult> Post([FromForm] LessonDto dto)
        {
           
            string host = HttpContext.Request.Host.Value;
            host = "https://" + host+"//";

            dto.savingPort = host;

            //------------------------

            if (dto.dayNumber == -1)
            {
                dto.dayNumber = null;
            }
            LessonDto getdto = new LessonDto();
            getdto =   await _lessonRepository.AddLesson(dto);
           
            return Ok(new Response<LessonDto>(getdto, "Created Sucessfully"));
         

        }

        [HttpPost]
        [Route("AddMcqs")]
        public async Task<IActionResult> Post(List<mcqsDto> dto) 
        {
            _lessonRepository.AddMcqs(dto);
            return Ok(new Response<mcqsDto>( "Created Sucessfully"));
        }

        [HttpGet]
        [Route("GetAllLessons")]
        public IEnumerable<CourseCategory> GetAll(int courseCategoryId)
        {

            IEnumerable<CourseCategory> course = _lessonRepository.GetAllLesson(courseCategoryId);
            return course;
        }

        //[HttpGet]
        //[Route("GetLessonby")]

        [HttpGet]
        [Route("GetLessonById")]
        public LessonDto GetById(int id)
        {
            LessonDto dto = _lessonRepository.GetLessonById(id);
            return dto;
        }

        [HttpPost]
        [Route("updateLesson")]
        public async Task<LessonDto> updatelesson([FromForm] LessonDto dto)
        {
           
          await   _lessonRepository.UpdateLesson(dto);
            return dto;
        }
        [HttpPost]
        [Route("updateMcqs")]
        public IActionResult updateMcq(List<mcqsDto> dto)
        {
               _lessonRepository.updateMcqs(dto);
            return Ok(new Response<mcqsDto>("Created Sucessfully"));
        }
        [HttpPost]
        [Route("deleteMcqs")]
        public IActionResult deleteMcqs(int id)
        {
            _lessonRepository.deleteMcqs(id);
            return Ok(new Response<mcqsDto>("deleted Sucessfully"));
        }



        [HttpDelete]
        [Route("DeleteLesson")]
        public async Task<IActionResult> Delete(int id)
        {
            _lessonRepository.Delete(id);
            return Ok(new Response<int>(id, "Deleted Successfully"));
        }
    }
}
