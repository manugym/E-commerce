using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;
        private readonly OrderRepository _orderRepository;
        private readonly CartRepository _cartRepository;
        private readonly ProductRepository _productRepository;

        public OrderController(MyDatabase myDatabase, CartRepository cartRepository, ProductRepository productRepository, OrderRepository orderRepository)
        {
            _myDatabase = myDatabase;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost("CreateTemporaryOrder")]
        public async Task<ActionResult<TemporaryOrder>> TemporaryOrder([FromBody] ICollection<OrderDetailDto> orderDetailsDto)
        {
            if (orderDetailsDto == null || !orderDetailsDto.Any())
            {
                return BadRequest("Order details cannot be null or empty.");
            }

            var temporaryOrder = await _orderRepository.TemporaryOrder(orderDetailsDto);

            return Ok(temporaryOrder);
        }
    }
}
