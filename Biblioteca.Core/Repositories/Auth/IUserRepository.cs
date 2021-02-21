using Biblioteca.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Core.Repositories.Auth
{
    public interface IUserRepository : IRepository<User>
    {
        //Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}
