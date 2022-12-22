using AutoMapper;
using BeajLearner.Application.DTOs;
using BeajLearner.Application.DTOs.Account;
using BeajLearner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            //#region Supplier
            //CreateMap<Supplier, SupplierDTO>();
            //CreateMap<CreateSupplierDTO, Supplier>();
            //CreateMap<UpdateSupplierDTO, Supplier>();
            //#endregion
            CreateMap<CourseCategory, getAllCategoriesDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Lesson, GetLessonsDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<TeachersAssignedCourse, AssignTeacherDto>().ReverseMap();
          //  CreateMap<UserSignup, UserSignUpDTO>().ReverseMap();
            CreateMap<RegisterRequest, CustomerSignupDto>().ReverseMap();
            CreateMap<mcqQuestions,mcqsDto>().ReverseMap();
            CreateMap<Otp, otpDto>().ReverseMap();
            CreateMap<purchasedCourse,purchaseCourseDto>().ReverseMap();
           
        }
    }
}
