using BeajLearner.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs
{
    public class sendOtpDto
    {
       public string receiver { get; set; }
        public string textmessage { get; set; }
        public  networkCarrier networkCarrier { get; set; }
    }
}
