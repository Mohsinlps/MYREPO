
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using BeajLearner.Infrastructure.Identity.Contexts;
using BeajLearner.Application.Interfaces.Repositories;

namespace BeajLearner.Infrastructure.Identity.Repositories
{
    public class RoleRepository : EfRepository<IdentityRole>, IRoleRepository
    {
        public readonly IdentityContext _dbContext;

        public RoleRepository(IdentityContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetByEmail(string email)
        {

            return await (from role in _dbContext.Roles
                          join userRole in _dbContext.UserRoles on role.Id equals userRole.RoleId
                          join user in _dbContext.Users on userRole.UserId equals user.Id
                          where user.NormalizedEmail.Equals(email.ToUpper())
                          select role.Name).ToListAsync();
        }


        public async Task<IEnumerable<string>> GetByUserId(string id)
        {
            return await (from role in _dbContext.Roles
                          join userRole in _dbContext.UserRoles on role.Id equals userRole.RoleId
                          join user in _dbContext.Users on userRole.UserId equals user.Id
                          where user.Id.Equals(id)
                          select role.Name).ToListAsync();

        }


    }
}
