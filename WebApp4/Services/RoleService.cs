using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp4.Areas.Identity.Data;
using WebApp4.Models;

namespace WebApp4.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManger;

        public RoleService(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManger)
        {
            this.roleManager = roleManager;
            this.userManger = userManger;
        }

        public async Task<bool> Add(RoleDto roleDto)
        {
            var identityRole = new IdentityRole
            { 
                Name = roleDto.Name,
                NormalizedName = roleManager.NormalizeKey(roleDto.Name), 
                 
            };

            var result = await roleManager.CreateAsync(identityRole);

            if (!result.Succeeded) return false;

            return true;

        }

        public async Task<List<IdentityRole>> GetAll()
        {
            var result = await roleManager.Roles.ToListAsync();
            return result;
        }

    }
}
