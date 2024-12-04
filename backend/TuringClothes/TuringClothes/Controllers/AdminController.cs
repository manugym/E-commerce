using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private MyDatabase _mydatabase;
        public AdminController(MyDatabase myDatabase) 
        {
            _mydatabase = myDatabase;
        }

        [HttpGet("getAllUsers")]
        public IActionResult GetUsers()
        {
            var users = _mydatabase.Users
                .Select(user => new
                {
                    user.Id,
                    user.Name,
                    user.Surname,
                    user.Email,
                    user.Role
                })
                .ToList();

            return Ok(users);
        }

        [HttpGet("getAllProducts")]
        public IActionResult GetProducts()
        {
            var products = _mydatabase.Products
                .Select(product => new
                {
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Stock,
                    product.Image
                })
                .ToList();

            return Ok(products);
        }
    }
}
