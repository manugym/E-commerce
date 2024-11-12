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
        public async Task<IActionResult> GetCart(long id)
        {
            var cart = await _cartRepository.GetCart(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
    }
}
