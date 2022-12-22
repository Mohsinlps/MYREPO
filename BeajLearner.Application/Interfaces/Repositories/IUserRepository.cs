
using BeajLearner.Application.DTOs;
using BeajLearner.Application.DTOs.User;
using BeajLearner.Application.Wrappers;
using BeajLearner.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface IUserRepository : IAsyncRepository<IdentityUser>
    {
        //Task<IReadOnlyList<User>> GetAllAsync(string search, string orderBy, bool ascending);

        public Task<string> getUserByUserId(string userName);
        public  Task<getCustomerProfileDto> getUserProfile(string userId);
        //---------------------------

        Task<Response<bool>> DeleteUserAsync(string id);

        Task<Response<string>> UpdateUserAsync(UpdateUserRequest userRequest);

        Task<bool> IsInRoleAsync(User user, string role);
        Task<Response<IEnumerable<Role>>> GetAllRoles();
        Task<Response<IEnumerable<User>>> GetAllUsers();
        Task<Response<User>> GetUserById(string id);
        Task<PagedResponse<IEnumerable<User>>> GetAllUsersPaged(int pageNo, int pageSize, string searchParam);
        //Task<Response<bool>> UploadUserDocument(UploadUserDocumentDTO request);

    }
}
