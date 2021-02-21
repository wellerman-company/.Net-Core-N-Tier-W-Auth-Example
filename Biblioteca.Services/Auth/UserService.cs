using Biblioteca.Core;
using Biblioteca.Core.Models.Auth;
using Biblioteca.Core.Services.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Register new user
        public async Task<User> Register(User user, string password,List<string> roles)
        {

            var userWithSameEmail = await _userManager.FindByEmailAsync(user.Email);

            // Check if email already exits - If Not
            if (userWithSameEmail == null)
            {
                // Create
                var userCreateResult = await _userManager.CreateAsync(user, password);

                // If create ok
                if (userCreateResult.Succeeded)
                {

                    // Add roles to user
                    foreach (var role in roles)
                        await _userManager.AddToRoleAsync(user, role);

                    // OK
                    return await _userManager.FindByEmailAsync(user.Email);
                }
                else
                    return new User();

            }
            else
                return new User();
        }

        public async Task<string> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return "Role name should be provided.";
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
                return "Ok";

            return roleResult.Errors.First().Description;
        }


    }
}
