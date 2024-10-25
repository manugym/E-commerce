using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _authRepository;


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


    }
}
