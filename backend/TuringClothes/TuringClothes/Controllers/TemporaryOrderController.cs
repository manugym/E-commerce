using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Repository;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryOrderController : ControllerBase
    {
        private readonly CartRepository _cartRepository;
        private readonly ProductRepository _productRepository;
        private readonly TemporaryOrderRepository _temporaryOrderRepository;

        public TemporaryOrderController(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;

        }

        [Authorize]
        [HttpPost("Receive-cart")]
        public async Task<IActionResult> GetLocalCartOrder([FromBody] ICollection<CartDetail> cartDetails)
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            if (cartDetails == null)
            {
                return BadRequest("El carrito enviado está vacío o no es válido.");
            }

            await _temporaryOrderRepository.AddTemporaryOrderAsync(userIdLong, cartDetails);
            
            return Ok(new
            {
                message = "Carrito procesado correctamente",
                itemsProcessed = cartDetails.Count
            });
        }

    }
}
