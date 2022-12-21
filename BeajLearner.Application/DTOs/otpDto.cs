using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class otpDto
    {
        public string recieverNumber { get; set; }
        public int otp { get; set; }
        public DateTime StartTime { get; set; }
    }
}
