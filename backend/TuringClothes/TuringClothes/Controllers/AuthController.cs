using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [HttpGet("GetAllUsers")]
        public async Task<IEnumerable<User>> GetAllUsersSinConvertir()
        {
            IEnumerable<User> users = await _authRepository.GetAllUsersAsync();

            return users;
        }

        [HttpGet("GetByEmail")]
        public async Task<IEnumerable<User>> GetByEmail(string mail)
        {
            var user = await _authRepository.GetByEmail(mail);
            return user;

        }

        [HttpGet("ToDto")]
        public async Task<IEnumerable<AuthDto>> GetAllUsersConvertidos()
        {
            IEnumerable<User> userAConvertir = await _authRepository.GetAllUsersAsync();
            
            

            IEnumerable<AuthDto> usersConvertidos = _authMapper.ToDto(userAConvertir);

            return usersConvertidos;
        }
    }
}
