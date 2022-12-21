using BeajLearner.Application.DTOs.Account;
using BeajLearner.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        //----------------------------------------------------------------------------
        public  Task<Response<string>> FindDuplicateUser(string UserName);
        Task<Response<string>> RegisterAsyncUser(RegisterRequest request, string origin);
        public Task<Response<string>> GenerateCustomerProfile(GenerateCustomerProfileDto dto, string origin);
        public Task<Response<AuthenticationResponse>> AuthenticateAsyncUser(AuthenticationRequest request, string ipAddress);
        //----------------------------------------------------------------------------

        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<Response<AuthenticationResponse>> RefreshTokenAsync(string token, string ipAddress);

    }
}
