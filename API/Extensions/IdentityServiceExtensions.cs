using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            //services.AddIdentityCore<AppUser>(opt =>
            //{
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequireLowercase = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequiredLength = 4;

            //    opt.Lockout.AllowedForNewUsers = false;
            //}).AddDefaultTokenProviders();
            return services;
        }
    }
}