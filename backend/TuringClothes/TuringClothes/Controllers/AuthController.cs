using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly TokenValidationParameters _tokenParameters;
        private readonly AuthRepository _authRepository;
        private readonly MyDatabase _myDatabase;

        public AuthController(MyDatabase myDatabase, IOptionsMonitor<JwtBearerOptions> jwtOptions, AuthRepository authRepository)
        {
            _authRepository = authRepository;
            _myDatabase = myDatabase;
            _tokenParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginData)
        {
            var user = await _authRepository.GetByEmail(loginData.Email);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            //si el usuario existe creamos su token
            if (loginData.Email == user.Email && BCrypt.Net.BCrypt.Verify(loginData.Password, user.Password))
            {
               string stringToken = GenerateToken(user);

                return Ok(new AuthResultDto { AccessToken = stringToken });
            }
            //si el usuario no existe, se le notifica que uno de los campos es incorrecto por seguridad
            return Unauthorized("Email o contraseña incorrecto");
        }


        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerData)
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
                Role = "user"
            };
            await _myDatabase.Users.AddAsync(newUser);
            await _myDatabase.SaveChangesAsync();
            string stringToken = GenerateToken(newUser);

            return Ok(new AuthResultDto { AccessToken = stringToken });
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetSecret()
        {
            // Envuelve el string en un objeto JSON
            return Ok(new { message = "Esto es un secreto que nadie puede saber..." });
        }

        [HttpGet("user by email")]
        public async Task<User?> GetUserByEmail(string mail)
        {
            var user = await _authRepository.GetByEmail(mail);
            return user;
        }

        private string GenerateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //datos que sirven para autorizar al usuario
                Claims = new Dictionary<string, object>
                    {
                        { "id", user.Id },
                        { ClaimTypes.Name, user.Name},
                        { ClaimTypes.Email, user.Email },
                        { ClaimTypes.Role, user.Role }

                    },
                //caducidad del Token
                Expires = DateTime.UtcNow.AddYears(1),
                //la clave y el algoritmo de firmado
                SigningCredentials = new SigningCredentials(
                       _tokenParameters.IssuerSigningKey,
                       SecurityAlgorithms.HmacSha256Signature)
            };
            //con el token creado, se le devuelve al usuario logeado
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }



}

