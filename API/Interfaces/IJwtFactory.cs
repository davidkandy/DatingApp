using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Options;
using API.Entities;


namespace API.Services
{
    public interface IJwtFactory
    {
        JwtIssuerOptions Options { get; }
        Task<string> GenerateToken(AppUser user);
    }
}
