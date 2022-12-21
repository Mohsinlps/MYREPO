
using BeajLearner.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface IRoleRepository : IAsyncRepository<IdentityRole>
    {
        Task<IEnumerable<string>> GetByUserId(string id);
        Task<IEnumerable<string>> GetByEmail(string email);
    }
}
