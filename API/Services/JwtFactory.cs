using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using API.Entities;
using API.Extensions.UnityExtensions;
using API.Models.Options;
namespace API.Services
{
    public class JwtFactory : IJwtFactory
    {
        #region Properties
        public JwtIssuerOptions Options => IssuerOptions.Value;

        #region Internals

        // [DeepDependency]
        UserManager<AppUser> UserManager { get; }

        // [DeepDependency]
        IOptions<JwtIssuerOptions> IssuerOptions { get; }

        public const string JWT_CLAIM_ID = "id";
        public const string JWT_CLAIM_ROL = "rol";
        public const string JWT_CLAIM_ROLES = "roles";
        public const string JWT_CLAIM_VERIFIED = "ver";

        #endregion

        #endregion

        #region Methods

        #region IJwtFactory Implementation

        public async Task<string> GenerateToken(AppUser user)
        {
            IList<string> roles = await UserManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault();
            
            var identity = new ClaimsIdentity(new GenericIdentity(user.Id.ToString(), "Token"), new[]
            {
                new Claim(JWT_CLAIM_ID, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await Options.JtiGenerator()),
                new Claim(JWT_CLAIM_ROL, role),
                new Claim(JWT_CLAIM_ROLES, string.Join(",", roles)),
                // new Claim(Core.JWT_CLAIM_VERIFIED, (user.EmailConfirmed && user.PhoneNumberConfirmed).ToString())
            });

            if (role == UserRoles.Administrator.ToString())
                Options.ValidFor = TimeSpan.FromHours(1);

            return new JwtSecurityTokenHandler().CreateEncodedJwt(
                Options.Issuer, Options.Audience, identity,
                Options.NotBefore, Options.Expiration, Options.IssuedAt,
                Options.SigningCredentials);
        }
        #endregion

        #endregion
    }
}
