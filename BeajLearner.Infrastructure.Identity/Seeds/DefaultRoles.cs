using BeajLearner.Application.Enums;
using BeajLearner.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Identity.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.CentralAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.RegionalLeads.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Mentors.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.CenterOwners.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teachers.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Students.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Parents.ToString()));
        }
    }
}
