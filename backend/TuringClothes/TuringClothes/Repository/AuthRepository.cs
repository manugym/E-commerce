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
 
        
    }
}
