using BeajLearner.Application.DTOs.Account;
using BeajLearner.Application.DTOs.User;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using BeajLearner.WebApp.Sessions;

namespace BeajLearner.WebApi.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        public UserController(IAccountService accountService, IUserRepository userRepository)
        {
            _accountService = accountService;
            _userRepository = userRepository;
           
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }

        [HttpGet]
        [Route("AllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _userRepository.GetAllRoles());
        }
        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> AllUsers( )
        {
            return Ok(await _userRepository.GetAllUsers());
        }
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _userRepository.GetUserById(id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _userRepository.DeleteUserAsync(id));
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            return Ok(await _userRepository.UpdateUserAsync(request));
        }
        [HttpGet]
        [Route("AllUsersPaged")]
        public async Task<IActionResult> GetAllUsersPaged(int PageNumber, int PageSize, string SearchParam)
        {
            return Ok(await _userRepository.GetAllUsersPaged(PageNumber, PageSize, SearchParam));
        }
        //[HttpPost]
        //[Route("UploadUserDocuments")]
        //public async Task<IActionResult> UploadUserDocuments([FromBody] UploadUserDocumentDTO request)
        //{
        //    return Ok(await _userRepository.UploadUserDocument(request));
        //}

    }
}
