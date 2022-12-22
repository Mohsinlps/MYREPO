using AutoMapper;
using BeajLearner.Application.DTOs.Account;
using BeajLearner.Application.DTOs.Email;
using BeajLearner.Application.Exceptions;
using BeajLearner.Application.Interfaces;
using BeajLearner.Application.Wrappers;
using BeajLearner.Domain.Settings;
using BeajLearner.Infrastructure.Identity.Contexts;
using BeajLearner.Infrastructure.Identity.Helpers;
using BeajLearner.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public AccountService(IdentityContext identityContext, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IConfiguration configuration,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService)
        {
            _identityContext = identityContext;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _configuration = configuration;
            this._emailService = emailService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            try
            {
                var user = await _userManager.Users.Include(x => x.RefreshTokens).Where(x => x.Email == request.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new ApiException($"No Accounts Registered with {request.Email}.");
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    throw new ApiException($"Invalid Credentials for '{request.Email}'.");
                }
                if (!user.EmailConfirmed)
                {
                    throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
                }
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                AuthenticationResponse response = new AuthenticationResponse();
                response.Id = user.Id;
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.Email = user.Email;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                //response.EmployeeNo = user.EmployeeNo;
                response.UserDocument = user.Documents;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Role = rolesList.FirstOrDefault();
                response.IsVerified = user.EmailConfirmed;
                var refreshToken = GenerateRefreshToken(ipAddress);
                response.RefreshToken = refreshToken.Token;
                if (user.RefreshTokens is null)
                {
                    user.RefreshTokens = new List<RefreshToken>();
                }
                user.RefreshTokens = new List<RefreshToken>();
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
                return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                EmailConfirmed = true,
                //CityId=request.CityId,
                //EmployeeNo=request.EmployeeNo,
                PhoneNumberConfirmed = true
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, request.Role);
                    //var verificationUri = await SendVerificationEmail(user, origin);
                    //TODO: Attach Email Service here and configure it via appsettings
                    //await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@logicps.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                    return new Response<string>(user.Id, Message: $"User Registered Successfully.");
                }
                else
                {
                    StringBuilder errorMessage = new StringBuilder();
                    if (result != null && result.Errors.Count() > 0)
                    {
                        foreach (var message in result.Errors)
                        {
                            if (message != null && message.Description != null)
                                errorMessage.AppendLine(message.Description);
                        }
                    }
                    throw new ApiException($"{errorMessage}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email} is already registered.");
            }
        }



        //*************************************************************

       // --------------------Find Duplicate User----------------------
        public async Task<Response<string>> FindDuplicateUser(string num)
        {
            var user = new ApplicationUser
            {
              
                UserName = num,
               
            };

            var usernameduplicate = await _userManager.FindByNameAsync(user.UserName);
          
            if (usernameduplicate == null)
            {
                return new Response<string>( Message: $"User Registered Successfully.");
            }
            else
            {
                return new Response<string>( Message: $"User Registered Successfully.");
            }
           
        }




        public async Task<Response<string>> RegisterAsyncUser(RegisterRequest request, string origin)
        {

            var user = new ApplicationUser
            {
                Email = request.Email,
                // FirstName = request.FirstName,
                //  LastName = request.LastName,
                UserName = request.CellNumber,
                //  EmailConfirmed = true,

                CellNumber = request.CellNumber,
                //DOB = request.DOB.ToString(),
                //CityId=request.CityId,
                //EmployeeNo=request.EmployeeNo,
                PhoneNumberConfirmed = true
            };


            var usernameduplicate = await _userManager.FindByNameAsync(user.UserName);
            //    var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (usernameduplicate == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, request.Role);
                    //var verificationUri = await SendVerificationEmail(user, origin);
                    //TODO: Attach Email Service here and configure it via appsettings
                    //await _emailService.SendAsync(new Application.DTOs.Email.EmailRequest() { From = "mail@logicps.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                    return new Response<string>(user.Id, Message: $"User Registered Successfully.");
                }
                else
                {
                    StringBuilder errorMessage = new StringBuilder();
                    if (result != null && result.Errors.Count() > 0)
                    {
                        foreach (var message in result.Errors)
                        {
                            if (message != null && message.Description != null)
                                errorMessage.AppendLine(message.Description);
                        }
                    }
                    throw new ApiException($"{errorMessage}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email} is already registered.");
            }
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsyncUser(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.Users.Include(x => x.RefreshTokens).Where(x => x.CellNumber == request.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ApiException($"No Accounts Registered with {request.Email}.");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"Invalid Credentials for '{request.Email}'.");
            }
            //if (!user.EmailConfirmed)
            //{
            //    throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
            //}
            JwtSecurityToken jwtSecurityToken = await GenerateJWTokenUser(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            // response.Email = user.Email;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.UserName = user.UserName;
            //response.EmployeeNo = user.EmployeeNo;
            response.UserDocument = user.Documents;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Role = rolesList.FirstOrDefault();
            //response.IsVerified = user.EmailConfirmed;
            if (user.UserName != null)
            {
                response.IsVerified = true;
            }
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            if (user.RefreshTokens is null)
            {
                user.RefreshTokens = new List<RefreshToken>();
            }
            user.RefreshTokens = new List<RefreshToken>();
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");

        }
        public async Task<Response<string>> GenerateCustomerProfile(GenerateCustomerProfileDto dto, string origin)
        {
            var user = await _userManager.Users.Include(x => x.RefreshTokens).Where(x => x.UserName == dto.UserName).FirstOrDefaultAsync();
            user.DOB = dto.DOB;
            user.Gender = dto.Gender;
            user.Occupation = dto.Occupation;
            user.FirstName = dto.Name;
           // user.LastName = dto.LastName;
            var upd = await _userManager.UpdateAsync(user);
            return new Response<string>(user.UserName, Message: $"Profile Generated Successfully");
        }
       

        private async Task<JwtSecurityToken> GenerateJWTokenUser(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             //   new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("UserName",user.UserName),
                new Claim("fullName",String.Format("{0} {1}",user.FirstName,user.LastName)),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }



        //***************************************************************
        //***************************************************************




        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("fullName",String.Format("{0} {1}",user.FirstName,user.LastName)),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return new Response<string>(user.Id, Message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;
            var appURL = _configuration["appURL"];
            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
            var route = appURL + "account/reset-password?email=" + model.Email + "&token=" + code;
            //var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var emailRequest = new EmailRequest()
            {
                Body = $"Click on this link to reset your password - {route}",
                To = model.Email,
                Subject = "Reset Password",
            };
            await _emailService.SendAsync(emailRequest);
        }
        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
            var token = await _userManager.GeneratePasswordResetTokenAsync(account);
            var result = await _userManager.ResetPasswordAsync(account, token, model.Password);
            if (result.Succeeded)
            {
                return new Response<string>(model.Email, Message: $"Password Resetted.");
            }
            else
            {
                throw new ApiException($"Error occured while reseting the password.");
            }
        }
        public async Task<Response<AuthenticationResponse>> RefreshTokenAsync(string token, string ipAddress)
        {
            var authenticationModel = new AuthenticationResponse();

            var user = _userManager.Users.Include(x => x.RefreshTokens).FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                return new Response<AuthenticationResponse>($"Token did not match any users.");
            }

            var refreshToken = user.RefreshTokens.First(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                return new Response<AuthenticationResponse>($"Token Not Active.");
            }

            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;

            //Generate new Refresh Token and save to Database
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            //Generates new jwt
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            authenticationModel.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Role = rolesList.FirstOrDefault();
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.Id = user.Id;
            authenticationModel.Email = user.Email;
            authenticationModel.FirstName = user.FirstName;
            authenticationModel.LastName = user.LastName;
            //authenticationModel.EmployeeNo = user.EmployeeNo;
            authenticationModel.UserDocument = user.Documents;
            return new Response<AuthenticationResponse>(authenticationModel);
        }

    }
}
