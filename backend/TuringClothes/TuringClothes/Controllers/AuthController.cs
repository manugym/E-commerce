using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;

        public AuthController(MyDatabase myDatabase)
        {
            _myDatabase = myDatabase;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers() => _myDatabase.Users.ToList();


    }
}
