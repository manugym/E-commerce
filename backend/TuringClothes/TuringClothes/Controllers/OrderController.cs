using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Dtos;
using TuringClothes.Model;
using TuringClothes.Repository;


namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController (OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost("CreateOrder")]
        public async Task<Order> CreateOrder(OrderDto orderDto)
        {
            var order = await _orderRepository.CreateOrder(orderDto.OrderId, orderDto.PaymentMethod, orderDto.Status, orderDto.Total);
            return order;
        }

    }
}
