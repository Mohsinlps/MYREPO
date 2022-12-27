using AutoMapper;
using BeajLearner.Application.DTOs;
using BeajLearner.Application.DTOs.Account;
using BeajLearner.Application.Interfaces;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Application.Wrappers;
using BeajLearner.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSignupController : ControllerBase
    {
        
        private IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private IStudentsRepository _studentsRepository;
        public CustomerSignupController(IMapper mapper, IStudentsRepository studentRepo, IAccountService accountService, IUserRepository userRepository)
        {

            _accountService = accountService;
            _userRepository = userRepository;
            _mapper = mapper;
            _studentsRepository = studentRepo;

        }

        [AllowAnonymous]
        [HttpPost("registerCustomer")]
        public async Task<IActionResult> RegisterAsync([FromBody] CustomerSignupDto request)
        {
            string res = "";
            res = request.CellNumber.Substring(0, 1);
            if (res == "+")
            {
                request.CellNumber = request.CellNumber.Remove(0, 1);
            }
            res = request.CellNumber.Substring(0, 2);
            if (res == "92")
            {
                request.CellNumber = request.CellNumber.Remove(0, 2);
                request.CellNumber = "0" + request.CellNumber;
            }

            res = request.CellNumber.Substring(0, 1);
            if (res == "3")
            {
                request.CellNumber = "0" + request.CellNumber;
            }

            var origin = Request.Headers["origin"];
            RegisterRequest model = _mapper.Map<RegisterRequest>(request);
            model.Role = "Students";
            if (request.Password != request.ConfirmPassword)
            {
                var message = "password and confirmPassword does not match";
                return Ok(new ResponseOtp(message));
            }
            else
            {
                return Ok(await _accountService.RegisterAsyncUser(model, origin));
            }
        }

        
        [AllowAnonymous]
        [HttpPost("SignInCustomer")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            var origin = Request.Headers["origin"];
            string res = "";
            res = request.Email.Substring(0, 1);
            if (res == "+")
            {
                request.Email = request.Email.Remove(0, 1);
            }
            res = request.Email.Substring(0, 2);
            if (res == "92")
            {
                request.Email = request.Email.Remove(0, 2);
                request.Email = "0" + request.Email;
            }

            res = request.Email.Substring(0, 1);
            if (res == "3")
            {
                request.Email = "0" + request.Email;
            }

         //   var response = await _accountService.AuthenticateAsyncUser(request, origin);
            return Ok(await _accountService.AuthenticateAsyncUser(request, origin));
          //  return Ok(response);

        }


        [Authorize]
        [HttpPost("generateProfile")]
        public async Task<IActionResult> GenerateProfileAsync([FromBody]GenerateCustomerProfileDto dto)
        {
            string res = "";
            res = dto.UserName.Substring(0, 1);
            if (res == "+")
            {
                dto.UserName = dto.UserName.Remove(0, 1);
            }
            res = dto.UserName.Substring(0, 2);
            if (res == "92")
            {
                dto.UserName = dto.UserName.Remove(0, 2);
                dto.UserName = "0" + dto.UserName;
            }

            res = dto.UserName.Substring(0, 1);
            if (res == "3")
            {
                dto.UserName = "0" + dto.UserName;
            }
            var origin = Request.Headers["origin"];
            try
            {
               
                return Ok(await _accountService.GenerateCustomerProfile(dto, origin));
            }
            catch(Exception ex)
            {
                return Ok(new ResponseOtp("user not found"));
            }
          

           

        }

        [Authorize]
        [HttpPost("getCustomerProfile")]
        public async Task<getCustomerProfileDto> getCustomerProfile([FromBody] getProfileDto dto)
        {
           
            var origin = Request.Headers["origin"];
            try
            {
                string userid = dto.id;
                return (await _userRepository.getUserProfile(userid));
            }
            catch (Exception ex)
            {
                return null;
            }




        }


        [HttpPost("sendSmsWithNetwork")]
        public async Task< IActionResult> SendSMS(sendOtpDto sendotpdto)
        {
            //------------remove +----------------
            string res = "";
            res = sendotpdto.receiver.Substring(0, 1);
            if (res == "+")
            {
                sendotpdto.receiver = sendotpdto.receiver.Remove(0, 1);
            }
            res= sendotpdto.receiver.Substring(0, 2);
            if(res=="92")
            {
                sendotpdto.receiver = sendotpdto.receiver.Remove(0, 2);
                sendotpdto.receiver = "0" + sendotpdto.receiver;
            }

            res = sendotpdto.receiver.Substring(0, 1);
            if(res=="3")
            {
                sendotpdto.receiver = "0" + sendotpdto.receiver;
            }



            bool success = false;
            string userInfo = "";
            //----------Get Random digits---------------
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            string randomOTP= Convert.ToString(_rdm.Next(_min, _max));
            sendotpdto.textmessage ="Your authentication Code is "+ randomOTP;
            //------------------------

            string APIKey = "a5395d7e43b6a6d643c78bf1f43ab7ec";
            string sender = "8583";
            string URI = "https://api.veevotech.com/sendsms?" +
            "hash=" + APIKey +
            "&receivenum=" + sendotpdto.receiver +
            "&receivernetwork=" + sendotpdto.networkCarrier +
            "&sendernum=" + sender +
            "&textmessage=" + Uri.UnescapeDataString(sendotpdto.textmessage);
            try
            {
               
                //--------------Get already saved user with same number--------------
                string check= await   _studentsRepository.getDuplicate(sendotpdto.receiver);
               
                if(check== "not found")
                {
                    WebRequest req = WebRequest.Create(URI);
                    WebResponse resp = req.GetResponse();
                    var sr = new System.IO.StreamReader(resp.GetResponseStream());

                    otpDto dto = new otpDto();
                    dto.recieverNumber = sendotpdto.receiver;
                    dto.otp = Convert.ToInt32(randomOTP);

                    _studentsRepository.deleteOtp(sendotpdto.receiver);

                    _studentsRepository.saveOtp(dto);
                    success = true;
                    userInfo = "new";
                   
                    return Ok(new ResponseOtp(success, userInfo));
                }
                else 
                {
                   
                    success = true;
                    userInfo = "old";
                    return Ok(new ResponseOtp(success,userInfo));
                }
              
            }
            catch (WebException ex)
            {
               // var httpWebResponse = ex.Response as HttpWebResponse;
               // throw new UserFriendlyException(httpWebResponse.StatusCode.ToString());
            }

            return Ok(new ResponseOtp (success,"no response"));
        }

        //[HttpPost("sendSms")]
        //public string SendSMS2(string receiver, string textmessage)
        //{
        //    //----------Get Random digits---------------
        //    int _min = 1000;
        //    int _max = 9999;
        //    Random _rdm = new Random();
        //    string randomOTP = Convert.ToString(_rdm.Next(_min, _max));
        //    textmessage = "Your authentication Code is " + randomOTP;
        //    //------------------------
        //    string APIKey = "9a630e273f98c88387e971aa30878f3b";
        //    string sender = "8583";
        //    string URI = "https://api.veevotech.com/sendsms?" +
        //    "hash=" + APIKey +
        //    "&receivenum=" + receiver +

        //    "&sendernum=" + sender +
        //    "&textmessage=" + Uri.UnescapeDataString(textmessage);
        //    try
        //    {
        //        WebRequest req = WebRequest.Create(URI);
        //        WebResponse resp = req.GetResponse();
        //        var sr = new System.IO.StreamReader(resp.GetResponseStream());

        //        HttpContext.Session.SetString(receiver, randomOTP);
        //    }
        //    catch (WebException ex)
        //    {
        //        // var httpWebResponse = ex.Response as HttpWebResponse;
        //        // throw new UserFriendlyException(httpWebResponse.StatusCode.ToString());
        //    }
        //    return "";
        //}

        [HttpPost("authenticateOtp")]
        public IActionResult AuthenticateOtp(authenticateOtpDto authenticateDto)
        {
            string res = "";
            res = authenticateDto.recieverNum.Substring(0, 1);
            if (res == "+")
            {
                authenticateDto.recieverNum = authenticateDto.recieverNum.Remove(0, 1);
            }
            res = authenticateDto.recieverNum.Substring(0, 2);
            if (res == "92")
            {
                authenticateDto.recieverNum = authenticateDto.recieverNum.Remove(0, 2);
                authenticateDto.recieverNum = "0" + authenticateDto.recieverNum;
            }

            res = authenticateDto.recieverNum.Substring(0, 1);
            if (res == "3")
            {
                authenticateDto.recieverNum = "0" + authenticateDto.recieverNum;
            }




            DateTime currentTime = DateTime.Now;
            bool success = true;
            otpDto dto = new otpDto();
            dto = _studentsRepository.getOtpByNumber(authenticateDto.recieverNum);


            if (dto!=null) {
                

                bool otpExpiry = true;
               DateTime dateTimeNow= DateTime.UtcNow;
                dto.StartTime = Convert.ToDateTime(dto.StartTime);
               
                otpExpiry = dto.StartTime <= dateTimeNow;



                if (dto.recieverNumber == authenticateDto.recieverNum && dto.otp == authenticateDto.userOtp)
            {
                if (otpExpiry)
                {
                    success = false;
                    return Ok(new ResponseOtp("Otp Expired"));
                }
                else
                {
                      
                    success = true;
                    return Ok(new ResponseOtp(success,"matched"));
                }


            }
            else
            {
                success = false;

                return Ok(new ResponseOtp("not matched"));

            }

        }
            else
            {
                return Ok(new ResponseOtp("Reciever not found"));
            }
           
        }



    }
}
