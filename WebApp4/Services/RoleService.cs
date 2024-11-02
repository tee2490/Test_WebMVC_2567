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

        public async Task<IdentityRole> Find(string name)
        {
            var identityRole = await roleManager.FindByNameAsync(name);
            return identityRole;
        }

        public async Task<bool> Update(RoleUpdateDto roleUpdateDto)
        {
            var identityRole = await Find(roleUpdateDto.Name);

            if (identityRole == null) return false;

            identityRole.Name = roleUpdateDto.UpdateName;
            identityRole.NormalizedName = roleManager.NormalizeKey(roleUpdateDto.UpdateName);

            var result = await roleManager.UpdateAsync(identityRole);

            if (!result.Succeeded) return false;

            return true;
        }


    }
}
