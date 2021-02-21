using Biblioteca.Core.Models.Auth;
using Biblioteca.Core.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Data.Repositories.Auth
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private ApiDbContext ApiDbContext
        {
            get { return Context as ApiDbContext; }
        }

        public UserRepository(ApiDbContext context)
            : base(context)
        { }
    }
}
