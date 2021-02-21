using Biblioteca.Core.Constants;
using Biblioteca.Core.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.Contexts
{
    public class ApiDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new Role { Name = Authorization.Roles.Administrator.ToString() });
            await roleManager.CreateAsync(new Role { Name = Authorization.Roles.Client.ToString() });
            await roleManager.CreateAsync(new Role { Name = Authorization.Roles.Moderator.ToString() });
            await roleManager.CreateAsync(new Role { Name = Authorization.Roles.Dev.ToString() });
            await roleManager.CreateAsync(new Role { Name = Authorization.Roles.Employee.ToString() });
            //Seed Default User
            var defaultUser = new User { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
    }
}
