using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Api.Resources.Auth
{
    public class UserRoleResource
    {
        public  UserResource User { get; set; }
        public  RoleResource Role { get; set; }
    }
}
