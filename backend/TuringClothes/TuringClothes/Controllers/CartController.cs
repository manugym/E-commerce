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

        [Authorize]
        [HttpGet("GetCart")]
        public async Task<ActionResult> GetCart()
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            var cart = await _cartRepository.GetCart(userIdLong);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(cart);
        }

        [Authorize]
        [HttpPut("AddItem")]
        public async Task<ActionResult> AddItem(long id)
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            await _cartRepository.AddItemToCar(id, userIdLong);

            return Ok("Product added");
        }

        [Authorize]
        [HttpDelete("RemoveItem")]
        public async Task<ActionResult> RemoveItem(long id)
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            await _cartRepository.RemoveItemFromCart(id, userIdLong);

            return Ok("Product removed");
        }

        [Authorize]
        [HttpPost("UpdateItem")]
        public async Task<ActionResult> UpdateCart(long id, int amount)
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            var result = await _cartRepository.UpdateItemInCart(id, amount, userIdLong);

            if (!result)
            {
                return BadRequest("Failed to update the cart");
            }
                
            return Ok("Cart updated");
        }
    }
}
