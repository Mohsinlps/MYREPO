using BeajLearner.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface IStudentsRepository
    {

        public otpDto saveOtp(otpDto dto);
        public void deleteOtp(string reciever);
        public otpDto getOtpByNumber(string number);
        public Task<string> getDuplicate(string Number);
    }


}
