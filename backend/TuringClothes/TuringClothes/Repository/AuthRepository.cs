using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TuringClothes.Controllers;
using TuringClothes.Database;
using TuringClothes.Model;

namespace TuringClothes.Repository
{
    public class AuthRepository : Repository<User, int>
    {
        private readonly TokenValidationParameters _tokenParameters;
        private readonly MyDatabase _mydatabase;
        public AuthRepository(MyDatabase myDatabase, TokenValidationParameters tokenParameters) : base(myDatabase) 
        {
            _tokenParameters = tokenParameters;
            _mydatabase = myDatabase;
        }

        public async Task<User?> GetByEmail(string mail)
        {
           return await GetQueryable().FirstOrDefaultAsync(email => email.Email == mail);

        }

        public async Task <IEnumerable<User>> GetAllUsersAsync()
        {
            return await GetQueryable().ToArrayAsync();
        }

        public async Task<string> GenerateJwtToken(string mail, long userID)
        {
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>
                {
                    { ClaimTypes.Email, mail },
                    { "id", userID}
                },
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(
                    _tokenParameters.IssuerSigningKey,
                    SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}
