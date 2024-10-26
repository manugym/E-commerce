using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TuringClothes.Database;
using TuringClothes.Mapper;
using TuringClothes.Model;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private MyDatabase _database;
        private readonly AuthRepository _authRepository;
        private readonly AuthMapper _authMapper;
        private readonly TokenValidationParameters _tokenParameters;


        public AuthController(AuthRepository authRepository, AuthMapper authMapper)
        {
            _authRepository = authRepository;
            _authMapper = authMapper;
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IEnumerable<User>> GetAllUsersSinConvertir()
        {
            IEnumerable<User> users = await _authRepository.GetAllUsersAsync();

            return users;
        }

        [Authorize]
        [HttpGet("GetByEmail")]
        public async Task<IEnumerable<User>> GetByEmail(string mail)
        {
            var user = await _authRepository.GetByEmail(mail);
            return user;

        }

        [Authorize]
        [HttpGet("ToDto")]
        public async Task<IEnumerable<AuthDto>> GetAllUsersConvertidos()
        {
            IEnumerable<User> userAConvertir = await _authRepository.GetAllUsersAsync();
            
            

            IEnumerable<AuthDto> usersConvertidos = _authMapper.ToDto(userAConvertir);

            return usersConvertidos;
        }
    }
}
