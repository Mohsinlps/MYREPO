using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Wrappers
{
    public class ResponseOtp
    {

        public ResponseOtp()
        {
        }

        public ResponseOtp(string Message = null)
        {
            succeeded = false;
            message = Message;
        }

        public ResponseOtp(bool succeed,string Message)
        {
            succeeded = true;
            message = Message;
        }
        public bool succeeded { get; set; }
        public string message { get; set; }
   





    }




}
