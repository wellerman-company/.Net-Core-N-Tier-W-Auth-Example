using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Api.Resources.Auth
{
    public class UserSignUpResource
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
        public bool State { get; set; }

        public List<string> UserRoles { get; set; }

    }
}
