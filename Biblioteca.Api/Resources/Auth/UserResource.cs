using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Api.Resources.Auth
{
    public class UserResource
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public ICollection<UserRoleResource> UserRoles { get; set; }
    }
}
