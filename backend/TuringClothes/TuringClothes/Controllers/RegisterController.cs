using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;
        private readonly AuthRepository _authRepository;

        public RegisterController(MyDatabase myDatabase, AuthRepository authRepository)
        {
            _myDatabase = myDatabase;
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserDto registerData)
        {
            var existingUser = await _authRepository.GetByEmail(registerData.Email);
            if (existingUser != null)
            {
                return Conflict("El correo electrónico ya está registrado.");
            }

            // Hashea la contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerData.Password);
            var newUser = new User
            {
                Name = registerData.Name,
                Surname = registerData.Surname,
                Email = registerData.Email,
                Password = hashedPassword,
                Address = registerData.Address,
                Role = "User"
            };
            await _myDatabase.Users.AddAsync(newUser);
            await _myDatabase.SaveChangesAsync();
            return Ok("Usuario registrado");

        }

        [HttpGet]
        public async Task<User?> GetUserByEmail(string mail)
        {
            var user = await _authRepository.GetByEmail(mail);
            return user;
        }
    }
}
