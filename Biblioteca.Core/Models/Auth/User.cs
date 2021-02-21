using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Core.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string NIF { get; set; }
        public bool State { get; set; }

        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }


        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
