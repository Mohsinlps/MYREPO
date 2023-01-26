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
        public async Task<IActionResult> Post([FromForm]  LessonDto dto)
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

        //------------------------------
        [HttpPost]
        [Route("AddCourseWeekInfo")]
        public async Task<IActionResult> saveCourseWeekInfo([FromForm] courseWeekInfoDto dto)
        {
            string host = HttpContext.Request.Host.Value;
            host = "https://" + host + "//";

            dto.savingPort = host;

            LessonDto getdto = new LessonDto();

            
             _lessonRepository.addCourseWeekInfo(dto);

            return Ok(new Response<LessonDto>(getdto, "Created Sucessfully"));


        }

        [HttpGet]
        [Route("GetWeeksByCourseId")]
        public async Task<IEnumerable< CourseWeek>> GetWeeksByCourseId(int id)
        {
           
        IEnumerable<CourseWeek> weeks=   _lessonRepository.GetWeeksByCourseId(id);

            return weeks;

        }

        [HttpGet]
        [Route("GetWeekByCourseIdAndWeekNumber")]
        public async Task<CourseWeek> GetWeekByCourseIdAndWeekNumber(int id,int weekNumber)
        {

            CourseWeek weeks = _lessonRepository.GetWeeksByCourseIdAndWeekNumber(id,weekNumber);

            return weeks;

        }



        [HttpPost]
        [Route("AddSpeakActivityQuestions")]
        public async Task<IActionResult> addSpeakActivityQuestions([FromForm] speakActivityQuestionDto dto)
        {
            string host = HttpContext.Request.Host.Value;
            host = "https://" + host + "//";

            dto.savingPort = host;
            _lessonRepository.AddSpeakActivityQuestions(dto);
            return Ok(new Response<mcqsDto>("Created Sucessfully"));
        }


        [HttpPost]
        [Route("UpdateSpeakActivityQuestions")]
        public  IActionResult updateSpeakActivityQuestions([FromForm] updateSpeakActivityQuestionDto dto)
        {
            string host = HttpContext.Request.Host.Value;
            host = "https://" + host + "//";

            dto.savingPort = host;
            _lessonRepository.updateSpeakActivityQuestions(dto);
            return Ok(new Response<SpeakActivityQuestions>("Created Sucessfully"));
        }
        [HttpPost]
        [Route("UpdateSpeakActivityQuestionsWithoutAudio")]
        public void updateWithoutAudio([FromForm] updateSpeakActivityQuestionWithoutAudioDto dto)
        {
           
            _lessonRepository.updateSpeakActivityQuestionsWithoutAudio(dto);
          
        }




        [HttpPost]
        [Route("deleteSpeakActivityQuestions")]
        public void deleteSpeakActivityQquestions([FromBody] string[] toBeRemovedQuestions)
        {
            
                _lessonRepository.deleteSpeakActivityQuestions(toBeRemovedQuestions);
            
           

        }



        //-------------------  Add Document Files------------- 

        //[HttpPost]
        //[Route("AddDocumentFilesForLesson")]
        //public async Task<IActionResult> documentFiles([FromForm] List<documentFilesDto> dto)
        //{
        //    string host = HttpContext.Request.Host.Value;
        //    host = "https://" + host + "//";

        //    //   dto.Select(x=>x.savingPort=host);
        //    //  dto.savingPort = host;

        //    // _lessonRepository.AddMcqs(dto);
        //    return Ok(new Response<mcqsDto>("Created Sucessfully"));
        //}

        [HttpPost]
        [Route("AddDocumentFilesForLesson")]
        public void documentFiles([FromForm] documentFilesDto dto)
        {
            string host = HttpContext.Request.Host.Value;
            host = "https://" + host + "//";

            dto.savingPort = host;
            _lessonRepository.AddDocumentFiles(dto);

        }


        [HttpPost]
        [Route("UpdateDocumentFilesForLesson")]
        public void documentFilesUpdate([FromForm] documentFilesDto dto)
        {
            string host = HttpContext.Request.Host.Value;
            host = "https://" + host + "//";

            dto.savingPort = host;
            _lessonRepository.UpdateDocumentFiles(dto);

        }




        // -----------------   Add Mcqs -----------------------

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
        public GetLessonsDto GetById(int id)
        {
            GetLessonsDto dto = _lessonRepository.GetLessonById(id);
            return dto;
        }

        [HttpPost]
        [Route("updateLesson")]
        public async Task<LessonDto> updatelesson([FromForm] LessonDto dto)
        {

            string host = HttpContext.Request.Host.Value;
            host = "https://" + host + "//";

            dto.savingPort = host;
            await  _lessonRepository.UpdateLesson(dto);
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


        [HttpGet]
        [Route("addActivityAlias")]
        public IActionResult addAlias(string alias)
        {
            _lessonRepository.addActivityAlias(alias);
            return Ok(new Response<int>(1,"added Successfully"));
        }




        [HttpGet]
        [Route("getActivityAlias")]
        public IEnumerable<ActivityAlias> getAlias()
        {
            IEnumerable<ActivityAlias> activityalias = _lessonRepository.getActivityAlias();
            if (activityalias != null)
            {
                return activityalias;
            }
            else
            {
                return activityalias;
            }
        }
        [HttpGet]
        [Route("deleteActivityAlias")]
        public int deleteAlias(int id)
        {
            try
            {
                _lessonRepository.deleteActivityAlias(id);
                return id;
            }
            catch(Exception ex)

            {
                return 0;
            }
             
           
        }


        [HttpGet]
        [Route("DeleteLesson")]
        public IActionResult Delete(int id)
        {
            _lessonRepository.Delete(id);
            return Ok(new Response<int>(id, "Deleted Successfully"));
        }
    }
}
