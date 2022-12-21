using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T Data, string Message = null)
        {
            succeeded = true;
            message = Message;
            data = Data;
        }
        public Response(string Message)
        {
            succeeded = false;
            message = Message;
        }
        public bool succeeded { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; }
        public T data { get; set; }
    }
}
