using AutoMapper;
using BeajLearner.Application.DTOs;

using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.Entities;

using BeajLearner.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance.Repositories
{
    public class CourseCategoryRepository:ICourseCategoryRepository
    {

        private IMapper _mapper;
        private IAsyncRepository<CourseCategory> _repo;
        private readonly IConfiguration _configuration;
        public CourseCategoryRepository(IMapper mapper,IAsyncRepository<CourseCategory> repo,IConfiguration configuration)
        {
            _mapper = mapper;
            _repo = repo;
            _configuration = configuration;
        }

        //public IEnumerable<CourseCategory> GetAllRelated(int id)
        //{
        //    IEnumerable<CourseCategory> courseCategory = _repo.GetCourseCategoryAllData( id);

        //    return courseCategory;
        //}




        public CourseCategoryDto AddCourseCategory(CourseCategoryDto input)
        {
            string imagePath = "";
            //var fileDirectory = _configuration["FileSetting:DirectoryPath"];
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
                             file.CopyTo(fileStream);
                        }

                        #region save file name in lstDocument


                        //   lstimages.Add(filePath);
                      
                        imagePath = input.savingPort + "uploads/" + newFileName;
                        #endregion
                    }
                }

            
            }






            CourseCategory coursecat = new CourseCategory();
            coursecat.CourseCategoryName = input.CourseCategoryName;
            coursecat.image = imagePath;

            CourseCategory course = _repo.GetAll().Where(c => c.CourseCategoryName == input.CourseCategoryName).FirstOrDefault();
            if (course != null)
            {

            }
            else
            {
                coursecat = _repo.Add(coursecat);

            }








            //coursecat = _repo.Add(coursecat);
            input.CourseCategoryId = coursecat.CourseCategoryId;
            input.CourseCategoryName = coursecat.CourseCategoryName;
            return input;
        }

      
        public List< getAllCategoriesDto> GetAllCourseCategories()
        {
          IEnumerable<  CourseCategory> courseCategory = _repo.GetAll();
          
            List<getAllCategoriesDto> dto  = _mapper.Map<List< getAllCategoriesDto>>(courseCategory);
            return dto;
        }

       

        public CourseCategoryDto GetCourseCategoryById(int id)
        {
            CourseCategoryDto dto = new CourseCategoryDto();
            CourseCategory courseCategory=_repo.GetById(id);
            dto = _mapper.Map<CourseCategoryDto>(courseCategory);
            return dto;
        }

        public void UpdateCourseCategory(CourseCategoryDto dto)
        {
            CourseCategory courseCategory = _mapper.Map<CourseCategory>(dto);
            _repo.Update(courseCategory);
        }
        public void Delete(int id)
        {
            _repo.Delete(id);
          
        }
    }
}
