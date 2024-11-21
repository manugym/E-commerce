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

        [HttpPost("CreateTemporaryOrder")]
        public async Task<ActionResult<TemporaryOrder>> CreateTemporaryOrder([FromBody] ICollection<OrderDetailDto> orderDetailsDto)
        {
            if (orderDetailsDto == null || !orderDetailsDto.Any())
            {
                return BadRequest("Order details cannot be null or empty.");
            }

            var temporaryOrder = await _orderRepository.CreateTemporaryOrder(orderDetailsDto);

            return Ok(temporaryOrder);
        }

        [HttpGet("TemporaryOrder")]
        public async Task<ActionResult<TemporaryOrder>> GetTemporaryOrder(long id)
        {
            var temporaryOrder = _orderRepository.GetTemporaryOrder(id);
            return Ok(temporaryOrder);
        }
    }
}
