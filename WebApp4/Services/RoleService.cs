using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp4.Areas.Identity.Data;
using WebApp4.Models;

namespace WebApp4.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleService(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
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
            var identityRole = await Find(roleUpdateDto.Name); //ค้นหา role เก่า

            if (identityRole == null) return false;

            identityRole.Name = roleUpdateDto.UpdateName;
            identityRole.NormalizedName = roleManager.NormalizeKey(roleUpdateDto.UpdateName);

            var result = await roleManager.UpdateAsync(identityRole);

            if (!result.Succeeded) return false;

            return true;
        }

        public async Task<bool> Delete(string name)
        {
            var identityRole = await Find(name);

            if (identityRole == null) return false;

            //ตรวจสอบมีผู้ใช้บทบาทนี้หรือไม่
            var usersInRole = await userManager.GetUsersInRoleAsync(name);
            if (usersInRole.Count != 0) return false;

            var result = await roleManager.DeleteAsync(identityRole);

            if (!result.Succeeded) return false;

            return true;
        }



    }
}
