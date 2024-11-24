using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryOrderController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;
        private readonly TemporaryOrderRepository _orderRepository;
        private readonly CartRepository _cartRepository;
        private readonly ProductRepository _productRepository;

        public TemporaryOrderController(MyDatabase myDatabase, CartRepository cartRepository, ProductRepository productRepository, TemporaryOrderRepository orderRepository)
        {
            _myDatabase = myDatabase;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        [Authorize]
        [HttpPost("CreateTemporaryOrder")]
        public async Task<ActionResult<TemporaryOrder>> CreateTemporaryOrder([FromBody] ICollection<OrderDetailDto> orderDetailsDto)
        {
            if (orderDetailsDto == null || !orderDetailsDto.Any())
            {
                return BadRequest("Order details cannot be null or empty.");
            }

            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            var temporaryOrder = await _orderRepository.CreateTemporaryOrder(orderDetailsDto, userIdLong);

            return Ok(temporaryOrder);
        }

        [HttpGet("TemporaryOrder")]
        public async Task<ActionResult<TemporaryOrder>> GetTemporaryOrder(long id)
        {
            var temporaryOrder = _orderRepository.GetTemporaryOrder(id);
            return Ok(temporaryOrder);
        }

        [HttpPost("RefreshTemporaryOrders")]
        public async Task<ActionResult> RefreshTemporaryOrders([FromBody]long temporaryOrderId)
        {
            var temporaryOrder = await _orderRepository.GetTemporaryOrder(temporaryOrderId);
            if (temporaryOrder == null)
            {
                return NotFound("Temporary order no existe.");
            }
            temporaryOrder.ExpirationTime = DateTime.UtcNow.AddMinutes(1);
            await _myDatabase.SaveChangesAsync();
            return Ok("Temporary order expiration extendida.");
        }
    }
}
