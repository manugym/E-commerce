using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TuringClothes.Database;
using TuringClothes.Model;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly TokenValidationParameters _tokenParameters;
        private readonly MyDatabase _myDatabase;

        public AuthController(IOptionsMonitor<JwtBearerOptions> jwtOptions)
        {
            _tokenParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthDto loginData)
        {
            //si el usuario existe creamos su token
            if (loginData.Email == "juanmita@gmail.com" && loginData.Password == "123456")
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //datos que sirven para autorizar al usuario
                    Claims = new Dictionary<string, object>
                    {
                        { "id", Guid.NewGuid().ToString() },
                        { ClaimTypes.Email, loginData.Email },
                        { ClaimTypes.Role, loginData.Role }

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

        ////[Authorize]
        //[HttpGet("GetAllUsers")]
        //public async Task<IEnumerable<User>> GetAllUsersSinConvertir()
        //{
        //    IEnumerable<User> users = await _authRepository.GetAllUsersAsync();

        //    return users;
        //}

        ////[Authorize]
        //[HttpGet("GetByEmail")]
        //public async Task<User> GetByEmail(string mail, string password)
        //{
        //    var user = await _authRepository.GetByEmail(mail, password);
        //    return user;

        //}

        ////[Authorize]
        //[HttpGet("ToDto")]
        //public async Task<IEnumerable<AuthDto>> GetAllUsersConvertidos()
        //{
        //    IEnumerable<User> userAConvertir = await _authRepository.GetAllUsersAsync();



        //    IEnumerable<AuthDto> usersConvertidos = _authMapper.ToDto(userAConvertir);

        //    return usersConvertidos;
        //}

        //[HttpPost("Login")]
        //public async Task<ActionResult<string>> Login([FromBody] AuthDto data)
        //{
        //    var user = await _authRepository.GetByEmail(data.Email, data.Password);
        //    if (user == null)
        //    {
        //        return Unauthorized();
        //    }
        //    var token = await _authRepository.GenerateJwtToken(mail: user.Email, userID: user.Id);
        //    return Ok(token);
        //}
    }
}
