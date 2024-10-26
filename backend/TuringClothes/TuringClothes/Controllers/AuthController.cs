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
        private readonly AuthRepository _authRepository;
        private readonly AuthMapper _authMapper;


        public AuthController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet]
        public async Task<ICollection<User>> GetByEmail(string mail)
        {
            var user = await _authRepository.GetByEmail(mail);
            return user;
            
        }

        [HttpGet]
        public AuthDto ToDto(User users)
        {
            AuthDto autDto = _authMapper.ToDto(users);
            return autDto;
        }
    }
}
