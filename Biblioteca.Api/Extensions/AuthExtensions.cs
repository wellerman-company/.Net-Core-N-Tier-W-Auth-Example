using Biblioteca.Core.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Api.Extensions
{
    public static class AuthExtensions
    {
        //    public static IServiceCollection AddAuth(
        //        this IServiceCollection services,
        //        JwtSettings jwtSettings)
        //    {
        //        services.AddAuthentication(options =>
        //        {
        //            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        })

        //            .AddJwtBearer(o =>
        //            {
        //                o.RequireHttpsMetadata = false;
        //                o.SaveToken = false;
        //                o.TokenValidationParameters = new TokenValidationParameters
        //                {
        //                    ValidateIssuerSigningKey = true,
        //                    ValidateIssuer = true,
        //                    ValidateAudience = true,
        //                    ValidateLifetime = true,
        //                    ClockSkew = TimeSpan.Zero,

        //                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        //                };
        //            });

        //        return services;
        //    }

        //    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        //    {
        //        app.UseAuthentication();

        //        app.UseAuthorization();

        //        return app;
        //    }
        //}
    }
}
