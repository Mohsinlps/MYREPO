using BeajLearner.Application.DTOs.User;
using BeajLearner.Application.Exceptions;
using BeajLearner.Application.Interfaces;
using BeajLearner.Application.Interfaces.Repositories;
using BeajLearner.Application.Wrappers;
using BeajLearner.Infrastructure.Identity.Contexts;
using BeajLearner.Infrastructure.Identity.Models;
using BeajLearner.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BeajLearner.Application.Enums;

namespace BeajLearner.Infrastructure.Identity.Repositories
{
    public class UserRepository : EfRepository<IdentityUser>, IUserRepository
    {
        public readonly IdentityContext _dbContext;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IDocumentRepository _documentRepository;
        public UserRepository(/*IDocumentRepository documentRepository,*/ UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IdentityContext dbContext, IAuthenticatedUserService authenticatedUser) : base(dbContext)
        {
            _dbContext = dbContext;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
            _roleManager = roleManager;
            //_documentRepository = documentRepository;
        }
        public async Task<Response<IEnumerable<Role>>> GetAllRoles()
        {

            var query = _dbContext.Roles.AsQueryable();

            var allRoles = await query.ToListAsync();
            if (allRoles is null)
                throw new ApiException("Roles not found.");

            List<Role> roleList = new List<Role>();
            foreach (var role in allRoles)
            {
                roleList.Add(new Role() { Id = role.Id, Name = role.Name });
            }

            return new Response<IEnumerable<Role>>(roleList);
        }

        public Task<bool> IsInRoleAsync(User user, string role)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> UpdateUserAsync(UpdateUserRequest userRequest)
        {
            var isEmailExists = _dbContext.Users
                .Any(x => x.Email.ToLower() == userRequest.Email.ToLower() && x.Id != userRequest.Id);

            if (isEmailExists)
                throw new ApiException($"Email {userRequest.Email} is already registered.");

            var user = await _dbContext.Users.Where(x => x.Id == userRequest.Id).FirstOrDefaultAsync();
            if (user is null)
                throw new ApiException("User not found.");

            user.FirstName = userRequest.FirstName;
            user.LastName = userRequest.LastName;
            user.Email = userRequest.Email;
            user.UserName = userRequest.Email;
            //user.EmployeeNo = userRequest.EmployeeNo;
            //user.CityId = userRequest.CityId;
            user.NormalizedEmail = userRequest.Email.ToUpper();
            user.NormalizedUserName = userRequest.Email.ToUpper();
            _dbContext.Update(user);

            //Update role
            if (!String.IsNullOrEmpty(userRequest.Role))
            {
                var userRoles = _dbContext.UserRoles.Where(x => x.UserId == userRequest.Id);
                var deletedRoles = userRoles.ToList();

                foreach (var role in deletedRoles)
                {
                    _dbContext.Remove(role);
                }
                //await _userManager.AddToRoleAsync(user.Id, userRequest.Role);
                var roleFromDb = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == userRequest.Role);

                var UserRole = new IdentityUserRole<string>();
                UserRole.UserId = user.Id;
                UserRole.RoleId = roleFromDb.Id;
                await _dbContext.UserRoles.AddAsync(UserRole);
            }
            _dbContext.SaveChanges();
            return new Response<string>(userRequest.Id, "Updated successfully.");
        }

        //------------------------------Get user by username-----------------------

        public async Task<string> getUserByUserId(string userName)
        {
            var user =await  _dbContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return "not found";
            }
            else {
                return "aleady exist";
            }
           
        }
        //----------------------------------------------------------
        public async Task<Response<bool>> DeleteUserAsync(string id)
        {
            var user = await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
            return new Response<bool>(true, "Deleted successfully.");
        }

        public async Task<Response<IEnumerable<User>>> GetAllUsers()
        {
            var usersList = new List<User>();

            var query = (from u in _dbContext.Users
                         join userRole in _dbContext.UserRoles
                         on u.Id equals userRole.UserId
                         join r in _dbContext.Roles
                         on userRole.RoleId equals r.Id
                         select new User
                         {
                             Id = u.Id,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Email = u.Email,
                             //CityId = u.CityId,
                             Role = r.Name,
                             RoleId = r.Id,
                             //EmployeeNo = u.EmployeeNo
                         }).AsQueryable();
            usersList = await query.ToListAsync();
            return new Response<IEnumerable<User>>(usersList);
        }

        public async Task<Response<User>> GetUserById(string id)
        {
            var user = await (from u in _dbContext.Users
                              join userRole in _dbContext.UserRoles
                              on u.Id equals userRole.UserId
                              join r in _dbContext.Roles
                              on userRole.RoleId equals r.Id
                              where u.Id == id
                              select new User
                              {
                                  Id = u.Id,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  Email = u.Email,
                                  //CityId = u.CityId,
                                  Role = r.Name,
                                  RoleId = r.Id,
                                  //EmployeeNo = u.EmployeeNo,
                                  UserDocument = u.Documents
                              }).FirstOrDefaultAsync();
            if (user is null)
                throw new ApiException("User not found.");

            return new Response<User>(user);
        }

        public async Task<PagedResponse<IEnumerable<User>>> GetAllUsersPaged(int pageNo, int pageSize, string searchParam)
        {
            pageNo = pageNo == 0 ? 1 : pageNo;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int TotalRecord = 0;

            var query = (from u in _dbContext.Users
                         join userRole in _dbContext.UserRoles
                         on u.Id equals userRole.UserId
                         join r in _dbContext.Roles
                         on userRole.RoleId equals r.Id
                         select new User
                         {
                             Id = u.Id,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Email = u.Email,
                             //CityId = u.CityId,
                             Role = r.Name,
                             RoleId = r.Id,
                             //EmployeeNo = u.EmployeeNo
                         }).AsQueryable();

            if (!string.IsNullOrEmpty(searchParam))
            {
                searchParam = searchParam.ToLower().Trim();
                query = query.Where(x =>
                (x.FirstName != null && x.FirstName.ToLower().Contains(searchParam))
                ||
                (x.LastName != null && x.LastName.ToLower().Contains(searchParam))
                ||
                (x.Email != null && x.Email.ToLower().Contains(searchParam))
                //||
                //(x.EmployeeNo.ToString().Contains(searchParam))
                );
            }
            TotalRecord = query.Count();
            var usersList = await query.Skip((pageNo - 1) * pageSize)
               .Take(pageSize).OrderBy(x => x.FirstName).ToListAsync();


            return new PagedResponse<IEnumerable<User>>(usersList, pageNo, pageSize, TotalRecord);

        }
        //public async Task<Response<bool>> UploadUserDocument(UploadUserDocumentDTO request)
        //{
        //    var document = new Document
        //    {
        //        File = request.File,
        //        FileName = request.FileName,
        //        Type = DocumentType.User.ToString()
        //    };
        //    await _documentRepository.AddAsync(document);

        //    var user = await _userManager.FindByIdAsync(request.UserId);

        //    var _userDocument = new UserDocumentsDTO
        //    {
        //        DocumentId = document.Id,
        //        FileName = request.FileName
        //    };
        //    user.Documents = _userDocument;
        //    await _userManager.UpdateAsync(user);
        //    return new Response<bool>(true, "File uploaded successfully.");

        //}
    }
}
