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

        [HttpGet("GetlessonByCourseId")]
        public IEnumerable<GetLessonsDto> GetlessonbycourseId(int id)
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


        [HttpPost("getLessonByCourseIdAndActivity")]
        public IEnumerable<GetLessonsDto> getLessonByCourseIdAndActivity( getLessonByCourseIdAndActivityDto Getdto)
        {
            getMcqsDto getmcqdto = new getMcqsDto();
            getmcqdto.courseId = Getdto.courseId;
            getmcqdto.activity = Getdto.activity;
            List<GetLessonsDto> dto = new List<GetLessonsDto>();

            try
            {
                IEnumerable<Lesson> model = _lessonRepo.GetLessonByCourseIdAndActivity(getmcqdto);
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




        //----------------------------------------------


        //[HttpPost("getLessonByCourseIdAndActivity")]
        //public IEnumerable<GetLessonsDto> getLessonForDataTables(getLessonByCourseIdAndActivityDto Getdto)
        //{
        //    getMcqsDto getmcqdto = new getMcqsDto();
        //    getmcqdto.courseId = Getdto.courseId;
        //    getmcqdto.activity = Getdto.activity;
        //    List<GetLessonsDto> dto = new List<GetLessonsDto>();

        //    try
        //    {
        //        IEnumerable<Lesson> model = _lessonRepo.GetLessonByCourseIdAndActivity(getmcqdto);
        //        if (model != null)
        //        {
        //            dto = _mapper.Map<List<GetLessonsDto>>(model);
        //            return dto;
        //        }
        //        else
        //        {
        //            return dto;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return dto;
        //}












        //---------------------------------------------











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

        [HttpGet("getLessonById")]
        public IEnumerable<GetLessonsDto> getLessonById(int id)
        {
            List<GetLessonsDto> dto = new List<GetLessonsDto>();

            try
            {
                IEnumerable<Lesson> model = _lessonRepo.GetLessonById(id);
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


        [HttpGet("getLessonByCourseIdAndWeekNumber")]
        public IEnumerable<GetLessonsDto> getLessonByCourseIdAndWeekNumber(int id,int weekNumber)
        {
            List<GetLessonsDto> dto = new List<GetLessonsDto>();

            try
            {
                IEnumerable<Lesson> model = _lessonRepo.GetLessonByCourseIdAndWeekNumber(id,weekNumber);
                if (model != null)
                {
                    dto = _mapper.Map<List<GetLessonsDto>>(model);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

            }

            return dto;
        }


        [HttpGet]
        [Route("GetLessonWithCourseAndCategoryById")]
        public IEnumerable<GetLessonWithCourseAndCategoryDto> GetLessonWithCourseAndCategoryById(int id)
        {
            IEnumerable<GetLessonWithCourseAndCategoryDto> dto = _lessonRepo.GetAllLessonWithCatAndCourseById(id);
            return dto;
        }



    }
}
