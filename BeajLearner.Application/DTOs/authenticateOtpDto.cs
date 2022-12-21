using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class authenticateOtpDto
    {
       public string recieverNum { get; set; }
       public int userOtp { get; set; }
    }
}
