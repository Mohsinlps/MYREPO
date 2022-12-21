using AutoMapper;
using BeajLearner.Application.DTOs;
using BeajLearner.Application.Interfaces;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Domain.Entities;
using BeajLearner.Domain.Interfaces;
using BeajLearner.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance.Repositories
{
    
    public class StudentsRepository:IStudentsRepository
    {
        private IAsyncRepository<Otp> _repoStudent;
       private IUserRepository _userRepo;
        private IMapper _mapper;
        public StudentsRepository(IAsyncRepository<Otp> repoStudent,IMapper mapper, IUserRepository userrepo)
        {
            _repoStudent = repoStudent;
            _mapper = mapper;
            _userRepo = userrepo;
        }


        public otpDto saveOtp(otpDto dto)
        {
            
            Otp model=_mapper.Map<Otp>(dto);
            string currentTimeString = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            DateTime currentTime = DateTime.Parse(currentTimeString);

            //----------------------------------
          
            DateTime timenow = DateTime.UtcNow;
            timenow = timenow.AddMinutes(1);
            string time = timenow.ToString();
          

            //----------------------------------

            //model.StartTime = currentTime.AddMinutes(1);
            model.StartTime = time;
           var success= _repoStudent.Add(model);
            return dto;
        }


        public void deleteOtp(string reciever)
        {
          int Id=  _repoStudent.GetAll().Where(x => x.recieverNumber == reciever).Select(x=>x.Id).FirstOrDefault();

            if (Id !=0 )
            {
                _repoStudent.Delete(Id);
            }
        }


        public async Task< string> getDuplicate(string Number)
        {
            string confirm =await _userRepo.getUserByUserId(Number);
            if (confirm == "not found") 
            {
                return "not found";
            }
            else 
            {
                return "aleady exist";
            }
        }

        public otpDto getOtpByNumber(string number)
        {
          Otp model=  _repoStudent.GetAll().Where(x => x.recieverNumber == number).FirstOrDefault();

            otpDto dto = _mapper.Map<otpDto>(model);
            return dto;
        }
    }
}
