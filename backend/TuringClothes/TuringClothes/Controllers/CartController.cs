using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository;

        public CartController(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;

        }

        [HttpGet]
        public async Task<ActionResult> GetCart(long id)
        {
            var cart = await _cartRepository.GetCart(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> AddItem(long id)
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            var cart = _cartRepository.AddItemToCar(id, userIdLong);

            return Ok("Product added");
        }
    }
}
