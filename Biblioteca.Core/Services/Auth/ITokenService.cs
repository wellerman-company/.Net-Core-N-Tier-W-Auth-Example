using Biblioteca.Core.Models.Auth;
using Biblioteca.Core.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Core.Services.Auth
{
    public interface ITokenService
    {
        Task<TokenRequest> GetToken(TokenRequest tokenRequest, JwtSettings jwtSettings);
        Task<bool> RevokeToken(string token);
        Task<Authentication> RefreshTokenAsync(string token, JwtSettings jwtSettings);
    }
}
