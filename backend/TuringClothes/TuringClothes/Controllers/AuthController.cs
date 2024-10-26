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
        

        public AuthController(AuthRepository authRepository, AuthMapper authMapper)
        {
            _authRepository = authRepository;
            _authMapper = authMapper;
        }


        //[Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IEnumerable<User>> GetAllUsersSinConvertir()
        {
            IEnumerable<User> users = await _authRepository.GetAllUsersAsync();

            return users;
        }

        //[Authorize]
        [HttpGet("GetByEmail")]
        public async Task<User> GetByEmail(string mail, string password)
        {
            var user = await _authRepository.GetByEmail(mail, password);
            return user;

        }

        //[Authorize]
        [HttpGet("ToDto")]
        public async Task<IEnumerable<AuthDto>> GetAllUsersConvertidos()
        {
            IEnumerable<User> userAConvertir = await _authRepository.GetAllUsersAsync();
            
            

            IEnumerable<AuthDto> usersConvertidos = _authMapper.ToDto(userAConvertir);

            return usersConvertidos;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] AuthDto data)
        {
            var user = await _authRepository.GetByEmail(data.Email, data.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = await _authRepository.GenerateJwtToken(mail: user.Email, userID: user.Id);
            return Ok(token);
        }
    }
}
