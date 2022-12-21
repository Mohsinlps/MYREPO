using AutoMapper;
using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.DomainDtos;
using BeajLearner.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetLessonsController : ControllerBase
    {

        private IMapper _mapper;
        private IGetLessonRepository _lessonRepo;
        public GetLessonsController(IGetLessonRepository lessonRepo, IMapper mapper)
        {
            _lessonRepo = lessonRepo;
            _mapper = mapper;
        }

        //[HttpGet("GetlessonByCourseId")]
        //public IEnumerable<GetLessonsDto> GetlessonbycourseId(int id)
        //{
        //    List<GetLessonsDto> dto = new List<GetLessonsDto>();

        //    try
        //    {
        //        IEnumerable<Lesson> model = _lessonRepo.GetLessonByCourseId(id);
        //        if(model != null)
        //        {
        //            dto = _mapper.Map<List<GetLessonsDto>>(model);
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


        [HttpPost("getLessonByCourseIdAndActivity")]
        public IEnumerable<GetLessonsDto> getLessonByCourseIdAndActivity([FromBody] getMcqsDto Getdto)
        {
            List<GetLessonsDto> dto = new List<GetLessonsDto>();

            try
            {
                IEnumerable<Lesson> model = _lessonRepo.GetLessonByCourseIdAndActivity(Getdto);
                if (model != null)
                {
                    dto = _mapper.Map<List<GetLessonsDto>>(model);
                    return dto;
                }
                else
                {
                    return dto;
                }
            }
            catch (Exception ex)
            {

            }

            return dto;
        }

        [HttpPost("getLessonByCourseId")]
        public IEnumerable<GetLessonsDto> getLessonByCourseId(int id)
        {
            List<GetLessonsDto> dto = new List<GetLessonsDto>();

            try
            {
                IEnumerable<Lesson> model = _lessonRepo.GetLessonByCourseId(id);
                if (model != null)
                {
                    dto = _mapper.Map<List<GetLessonsDto>>(model);
                    return dto;
                }
                else
                {
                    return dto;
                }
            }
            catch (Exception ex)
            {

            }

            return dto;
        }


    }
}
