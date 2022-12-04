using DentaClinic.Models;
using Microsoft.AspNetCore.Identity;

namespace DentaClinic.Utils
{
    public class AuthDbSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthDbSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddOdontologist();
        }

        private async Task AddOdontologist()
        {
            var newOdontologist = new User
            {
                UserName = "odonto@gmail.com",
                Email = "odonto@gmail.com",
                FirstName = "Petras",
                LastName = "Petraitis"
            };
            var exists = await _userManager.FindByNameAsync(newOdontologist.UserName);
            if(exists == null)
            {
                var createOdontologistResult = await _userManager.CreateAsync(newOdontologist, "Kristupas123?");
                if (createOdontologistResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newOdontologist, Roles.All);
                }
            }
        }

        private async Task AddDefaultRoles()
        {
            foreach (var item in Roles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(item);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }
    }
}
