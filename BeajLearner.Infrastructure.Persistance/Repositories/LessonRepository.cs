using AutoMapper;
using BeajLearner.Application.DTOs;
using BeajLearner.Application.Helpers;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.DomainDtos;
using BeajLearner.Domain.Entities;
using BeajLearner.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance.Repositories
{
    public class LessonRepository:ILessonRepository
    {
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        private IAsyncRepository<Lesson> _repo;
        private IAsyncRepository<mcqQuestions> _mcqRepo;
        private IAsyncRepository<CourseCategory> _repoCategory;
        private IAsyncRepository<Course> _repoCourse;
        private IAsyncRepository<CourseWeek> _repoCourseWeek;
        private IAsyncRepository<ActivityAlias> _repoActivityAlias;
        public LessonRepository(IAsyncRepository<ActivityAlias> activityalias, IAsyncRepository<CourseWeek> courseweek, IMapper mapper,IAsyncRepository<mcqQuestions> mcqRepo, IAsyncRepository<Lesson> repo, IAsyncRepository<Course> repoCourse, IAsyncRepository<CourseCategory> repoCourseCategory,IConfiguration configuration)
        {
            _mapper = mapper;
            _repo = repo;
            _repoCategory = repoCourseCategory;
            _configuration = configuration;
            _repoCourse = repoCourse;
            _mcqRepo = mcqRepo;
            _repoCourseWeek = courseweek;
            _repoActivityAlias = activityalias;
        }

        public async Task<LessonDto> AddLesson(LessonDto input)
        {
            Lesson lesson = new Lesson();
            lesson = _mapper.Map<Lesson>(input);


            #region
            // var fileDirectory = _configuration["FileSetting:DirectoryPath"];
            var fileDirectory = "wwwroot/uploads";
            var thumbnailDirectory = _configuration["FileSetting:Thumbnail:DirectoryPath"];
            var thumbnailWidth = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Width"]);
            var thumbnailHeight = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Height"]);


            //#region create folders
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            if (!Directory.Exists(thumbnailDirectory))
            {
                Directory.CreateDirectory(thumbnailDirectory);
            }
            //#endregion


            //#region save base64 to folder

            List<string> lstVideos = new List<string>();
            List<string> lstaudios = new List<string>();
            List<string> lstimages = new List<string>();





            if (input != null && input.videos != null)
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                foreach (IFormFile requestvideo in input.videos)
                {
                    if (requestvideo != null)
                    {


                        // create new guid and set file name with newly created guid.
                        var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + "mp4";


                        filePath = Path.Combine(GenericfilePath, newFileName);


                        IFormFile file = requestvideo;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }


                        #region save file name in lstDocument

                        string dbPath =input.savingPort+"uploads/"+ newFileName;
                        lstVideos.Add(dbPath);

                        #endregion
                    }
                }

                #region update files in product table in database

                if (lstVideos != null && lstVideos.Count > 0)
                {

                    lesson.videos = lstVideos.ToArray();

                }
                #endregion
            }
            #endregion
            if (input != null && input.Audios != null)
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                foreach (IFormFile requestaudio in input.Audios)
                {
                    if (requestaudio != null)
                    {


                        // create new guid and set file name with newly created guid.
                        var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + "mp3";


                        filePath = Path.Combine(GenericfilePath, newFileName);


                        IFormFile file = requestaudio;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        #region save file name in lstDocument

                        string dbPath = input.savingPort + "uploads/" + newFileName;
                        lstaudios.Add(dbPath);

                        #endregion
                    }
                }

                #region update files in product table in database

                if (lstaudios != null && lstaudios.Count > 0)
                {

                    lesson.Audios = lstaudios.ToArray();

                }
                #endregion
            }


            if (input != null && input.image != null)
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);

                int i = 0;
                foreach (IFormFile requestimage in input.image)
                {
                    if (requestimage != null)
                    {
                        var imgtype = input.image[i].ContentType;
                        i++;
                        imgtype = imgtype.Substring(imgtype.LastIndexOf('/') + 1);
                        // create new guid and set file name with newly created guid.
                        var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + imgtype;


                        filePath = Path.Combine(GenericfilePath, newFileName);


                        IFormFile file = requestimage;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        #region save file name in lstDocument

                        string dbPath = input.savingPort + "uploads/" + newFileName;
                        lstimages.Add(dbPath);

                        #endregion
                    }
                }

                #region update files in product table in database

                if (lstimages != null && lstimages.Count > 0)
                {

                    lesson.image = lstimages.ToArray();

                }
                #endregion
            }

            lesson = _repo.Add(lesson);
            LessonDto dto = new LessonDto();
            dto.LessonId = lesson.LessonId;
            return dto;

        }

        public void addCourseWeekInfo(courseWeekInfoDto input)
        {
            var fileDirectory = "wwwroot/uploads";
            if (input != null && input.image != null)
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
               
                  

                        // create new guid and set file name with newly created guid.
                        var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + "mp4";


                        filePath = Path.Combine(GenericfilePath, newFileName);


                        IFormFile file = input.image;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                             file.CopyTo(fileStream);
                        }


                        #region save file name in lstDocument

                        string dbPath = input.savingPort + "uploads/" + newFileName;
                CourseWeek courseWeek = new CourseWeek();
                courseWeek.image = dbPath;
                courseWeek.weekNumber = input.weekNumber;
                courseWeek.courseId = input.courseId;
                courseWeek.description = input.description;


                try
                {

    CourseWeek weekDuplicacy= _repoCourseWeek.GetAll().Where(x => x.weekNumber == input.weekNumber).FirstOrDefault();

                 
                    if (weekDuplicacy==null)
                    {
                       
                        _repoCourseWeek.Add(courseWeek);
                    }
                    else 
                    {
                        weekDuplicacy.courseId = input.courseId;
                        weekDuplicacy.weekNumber = input.weekNumber;
                        weekDuplicacy.description = input.description;
                        weekDuplicacy.image = dbPath;
                        _repoCourseWeek.Update(weekDuplicacy);
                    }

                  
                  
                  

                }
                catch (Exception ex) 
                {
                
                
                }
              
                        #endregion
                    
                

                #region update files in product table in database

              
                #endregion
            }

        }



        public IEnumerable<CourseWeek> GetWeeksByCourseId(int id)
        {
     IEnumerable< CourseWeek> weeks=      _repoCourseWeek.GetAll().Where(x => x.courseId == id).ToList();
           if(weeks!=null)
            {
                return weeks;
            }
            else { return null; }
        }

        public CourseWeek GetWeeksByCourseIdAndWeekNumber(int id,int weekNumber)
        {
           CourseWeek weeks = _repoCourseWeek.GetAll().Where(x => x.courseId == id && x.weekNumber==weekNumber).FirstOrDefault();
            if (weeks != null)
            {
                return weeks;
            }
            else { return null; }
        }


        //public async Task<LessonDto> AddLesson( LessonDto input)
        //{
        //    Lesson lesson = new Lesson();
        //    lesson = _mapper.Map<Lesson>(input);


        //    #region
        //    var fileDirectory = _configuration["FileSetting:DirectoryPath"];
        //    var thumbnailDirectory = _configuration["FileSetting:Thumbnail:DirectoryPath"];
        //    var thumbnailWidth = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Width"]);
        //    var thumbnailHeight = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Height"]);


        //    //#region create folders
        //    if (!Directory.Exists(fileDirectory))
        //    {
        //        Directory.CreateDirectory(fileDirectory);
        //    }
        //    if (!Directory.Exists(thumbnailDirectory))
        //    {
        //        Directory.CreateDirectory(thumbnailDirectory);
        //    }
        //    //#endregion


        //    //#region save base64 to folder

        //    List<string> lstVideos = new List<string>();
        //    List<string> lstaudios = new List<string>();
        //    List<string> lstimages = new List<string>();

        //    if (input != null && input.videos != null)
        //    {
        //        String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
        //        String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
        //        foreach (IFormFile requestvideo in input.videos)
        //        {
        //            if (requestvideo!=null)
        //            {


        //                // create new guid and set file name with newly created guid.
        //                var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + "mp4";


        //                filePath = Path.Combine(GenericfilePath, newFileName);


        //                IFormFile file = requestvideo;
        //                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                }


        //                #region save file name in lstDocument

        //                //   lstVideos.Add(newFileName);
        //                lstVideos.Add(filePath);

        //                #endregion
        //            }
        //        }

        //        #region update files in product table in database

        //        if (lstVideos != null && lstVideos.Count > 0)
        //        {

        //            lesson.videos = lstVideos.ToArray();

        //        }
        //        #endregion
        //    }
        //    #endregion
        //    if (input != null && input.Audios != null)
        //    {
        //        String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
        //        String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
        //        foreach (IFormFile requestaudio in input.Audios)
        //        {
        //            if (requestaudio != null)
        //            {


        //                // create new guid and set file name with newly created guid.
        //                var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + "mp3";


        //                filePath = Path.Combine(GenericfilePath, newFileName);


        //                IFormFile file = requestaudio;
        //                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                }

        //                #region save file name in lstDocument

        //                //   lstVideos.Add(newFileName);
        //                lstaudios.Add(filePath);

        //                #endregion
        //            }
        //        }

        //        #region update files in product table in database

        //        if (lstaudios != null && lstaudios.Count > 0)
        //        {

        //            lesson.Audios = lstaudios.ToArray();

        //        }
        //        #endregion
        //    }


        //    if (input != null && input.image != null)
        //    {
        //        String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
        //        String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);

        //        int i = 0;
        //        foreach (IFormFile requestimage in input.image)
        //        {
        //            if (requestimage != null)
        //            {
        //             var imgtype= input.image[i].ContentType;
        //                i++;
        //                 imgtype = imgtype.Substring(imgtype.LastIndexOf('/') + 1);
        //                // create new guid and set file name with newly created guid.
        //                var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + imgtype;


        //                filePath = Path.Combine(GenericfilePath, newFileName);


        //                IFormFile file = requestimage;
        //                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                }

        //                #region save file name in lstDocument

        //                //   lstVideos.Add(newFileName);
        //                lstimages.Add(filePath);

        //                #endregion
        //            }
        //        }

        //        #region update files in product table in database

        //        if (lstimages != null && lstimages.Count > 0)
        //        {

        //            lesson.image = lstimages.ToArray();

        //        }
        //        #endregion
        //    }

        //    lesson =  _repo.Add(lesson);
        //    LessonDto dto = new LessonDto();
        //    dto.LessonId = lesson.LessonId;
        //    return dto; 

        //}

        //-------------------------------   MCQS ------------------------------


        public void AddMcqs(List< mcqsDto> dto)
        {
            List<mcqQuestions> model = new List<mcqQuestions>();
            model = _mapper.Map<List<mcqQuestions>>(dto);

            try
            {
                foreach (var item in model)
                {
                    _mcqRepo.Add(item);
                }
            }
            catch(Exception ex)
            {

            }
           
        }
        public void deleteMcqs(int id)
        {
            List<mcqQuestions> deleteModel = new List<mcqQuestions>();
            deleteModel = _mcqRepo.GetAll().Where(x => x.lessonId == id).ToList();
            try
            {
                deleteModel = _mcqRepo.GetAll().Where(x => x.lessonId == id).ToList();
                if(deleteModel.Count > 0)
                {
                    _mcqRepo.deleteBulk(deleteModel);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void updateMcqs(List<mcqsDto> dto)
        {
            List<mcqQuestions> deleteModel = new List<mcqQuestions>();
        List<  mcqQuestions>  updateModel = _mapper.Map<List<mcqQuestions>>(dto);

            try
            {
                deleteModel =    _mcqRepo.GetAll().Where(x => x.lessonId == dto[0].lessonId).ToList();
                _mcqRepo.deleteBulk(deleteModel);

                foreach (var item in updateModel)
                {
                    _mcqRepo.Add(item);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public IEnumerable<GetLessonWithCourseAndCategoryDto> GetAllLessonWithCatAndCourse()
        {

       IQueryable<  GetLessonWithCourseAndCategoryDto> dto=   _repo.GetLessonWithCourseAndCategory();
            return dto;
        }


        public IEnumerable<CourseCategory> GetAllLesson(int courseCategoryId)
        {
           // Lesson lesson = new Lesson();

            var fileDirectory = _configuration["FileSetting:DirectoryPath"];
            var thumbnailDirectory = _configuration["FileSetting:Thumbnail:DirectoryPath"];
            var thumbnailWidth = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Width"]);
            var thumbnailHeight = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Height"]);


            List<string> lstVideos = new List<string>();
            List<string> lstaudios = new List<string>();


            String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);

      IEnumerable<CourseCategory> coursecategory= _repoCategory.GetCourseCategoryAllData(courseCategoryId);
            
            return coursecategory;
        }
        public GetLessonsDto GetLessonById(int id)
        {
            GetLessonsDto dto = new GetLessonsDto();
            Lesson Lesson = _repo.GetById(id);
            dto = _mapper.Map<GetLessonsDto>(Lesson);
            return dto;
        }

        

        //--------------  update -----------------
        public async Task<LessonDto> UpdateLesson(LessonDto input)
        {
            Lesson lesson = new Lesson();
            lesson = _mapper.Map<Lesson>(input);


            #region
            var fileDirectory ="wwwroot/uploads"; 
            var thumbnailDirectory = _configuration["FileSetting:Thumbnail:DirectoryPath"];
            var thumbnailWidth = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Width"]);
            var thumbnailHeight = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Height"]);


            //#region create folders
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            if (!Directory.Exists(thumbnailDirectory))
            {
                Directory.CreateDirectory(thumbnailDirectory);
            }
            //#endregion


            //#region save base64 to folder

            List<string> lstVideos = new List<string>();
            List<string> lstaudios = new List<string>();
            List<string> lstimages = new List<string>();

            if (input != null && input.videos != null)
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                foreach (IFormFile requestvideo in input.videos)
                {
                    if (requestvideo != null)
                    {


                        // create new guid and set file name with newly created guid.
                        var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + "mp4";


                        filePath = Path.Combine(GenericfilePath, newFileName);


                        IFormFile file = requestvideo;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }


                        #region save file name in lstDocument
                        string dbPath = input.savingPort + "uploads/" + newFileName;
                        lstVideos.Add(dbPath);

                        #endregion
                    }
                }

                #region update files in product table in database

                if (lstVideos != null && lstVideos.Count > 0)
                {

                    lesson.videos = lstVideos.ToArray();

                }
                #endregion
            }
            #endregion
            if (input != null && input.Audios != null)
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                foreach (IFormFile requestaudio in input.Audios)
                {
                    if (requestaudio != null)
                    {


                        // create new guid and set file name with newly created guid.
                        var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + "mp3";


                        filePath = Path.Combine(GenericfilePath, newFileName);


                        IFormFile file = requestaudio;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        #region save file name in lstDocument

                        string dbPath = input.savingPort + "uploads/" + newFileName;
                        lstaudios.Add(dbPath);

                        #endregion
                    }
                }

                #region update files in product table in database

                if (lstaudios != null && lstaudios.Count > 0)
                {

                    lesson.Audios = lstaudios.ToArray();

                }
                #endregion
            }


            if (input != null && input.image != null)
            {
                String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                String GenericfilePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);

                int i = 0;
                foreach (IFormFile requestimage in input.image)
                {
                    if (requestimage != null)
                    {
                        var imgtype = input.image[i].ContentType;
                        i++;
                        imgtype = imgtype.Substring(imgtype.LastIndexOf('/') + 1);
                        // create new guid and set file name with newly created guid.
                        var newFileName = string.Format(@"{0}", Guid.NewGuid()) + "." + imgtype;


                        filePath = Path.Combine(GenericfilePath, newFileName);


                        IFormFile file = requestimage;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        #region save file name in lstDocument

                        string dbPath = input.savingPort + "uploads/" + newFileName;
                        lstimages.Add(dbPath);

                        #endregion
                    }
                }

                #region update files in product table in database

                if (lstimages != null && lstimages.Count > 0)
                {

                    lesson.image = lstimages.ToArray();

                }
                #endregion
            }

            if(lesson.dayNumber==-1)
            {
                lesson.dayNumber = null;
            }
            lesson = _repo.Update(lesson);
            LessonDto dto = new LessonDto();
            dto.LessonId = lesson.LessonId;
            return dto;



        }


        public void Delete(int id)
        {
            _repo.Delete(id);

        }

        public string filepathglobal;
        public async Task testMethod(int courseId)
        {
            Lesson lesson = new Lesson();

            var fileDirectory = _configuration["FileSetting:DirectoryPath"];
            var thumbnailDirectory = _configuration["FileSetting:Thumbnail:DirectoryPath"];
            var thumbnailWidth = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Width"]);
            var thumbnailHeight = Convert.ToInt32(_configuration["FileSetting:Thumbnail:Height"]);





            //#region save base64 to folder

            List<string> lstVideos = new List<string>();
            List<string> lstaudios = new List<string>();


            String filePath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);

            IEnumerable<Lesson> dto = _repo.GetAll().Where(x => x.courseId == courseId);

        }



        public void addActivityAlias(string alias)
        {
            if (alias != null) 
            {
                ActivityAlias aliasModel=new ActivityAlias();
                aliasModel.Alias = alias;
              _repoActivityAlias.Add(aliasModel);
              

            }

        }
        public IEnumerable< ActivityAlias> getActivityAlias()
            {
                IEnumerable<ActivityAlias> aliasModel = _repoActivityAlias.GetAll();

                return aliasModel;
            }

        public void deleteActivityAlias(int id)
        {
            _repoActivityAlias.Delete(id);

           
        }


        // ---------------------------------------------------------

        //public List<String> GetAddresses(string sDir)
        //{
        //    List<String> files = new List<String>();
        //    foreach (string f in Directory.GetFiles(filepathglobal))
        //    {
        //        files.Add(f);
        //    }
        //    //foreach (string d in Directory.GetDirectories(filepathglobal))
        //    //{
        //    //    files.AddRange(DirSearch(d));
        //    //}
        //    return files;
        //}
        // ---------------------------------------------------------
    }
}
