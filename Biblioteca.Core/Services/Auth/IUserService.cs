using Biblioteca.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.Services.Auth
{
    public interface IUserService
    {
         Task<User> Register(User user, string password, List<string> roles);
         Task<string> CreateRole(string roleName);

    }
}
