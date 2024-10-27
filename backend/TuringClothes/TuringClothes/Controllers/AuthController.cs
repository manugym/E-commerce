using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] AuthDto loginData)
        {
            var user = await _authRepository.GetByEmail(loginData.Email);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            //si el usuario existe creamos su token
            if (loginData.Email == user.Email && loginData.Password == user.Password)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //datos que sirven para autorizar al usuario
                    Claims = new Dictionary<string, object>
                    {
                        { "id", Guid.NewGuid().ToString() },
                        { ClaimTypes.Email, loginData.Email },
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

                return Ok(new AuthResultDto { AccessToken = stringToken });
            }
            //si el usuario no existe, se le notifica que uno de los campos es incorrecto por seguridad
            return Unauthorized("Email o contraseña incorrecto");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public string GetSecret()
        {
            return "Esto es un secreto que no todo el mundo debería leer";
        }
    }



}

