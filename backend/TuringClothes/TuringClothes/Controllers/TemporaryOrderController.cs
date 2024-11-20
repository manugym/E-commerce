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

        public TemporaryOrderController(CartRepository cartRepository, TemporaryOrderRepository temporaryOrderRepository)
        {
            _cartRepository = cartRepository;
            _temporaryOrderRepository = temporaryOrderRepository;

        }

        [HttpGet("GetAllTemporaryOrders")]
        public async Task<IActionResult> GetAllTemporaryOrders()
        {
            var temporaryOrders = await _temporaryOrderRepository.GetAllTemporaryOrdersAsync();
            if (temporaryOrders == null)
            {
                return BadRequest("No existen ordenes temporales.");
            }
            return Ok(temporaryOrders);
        }

        //[Authorize]
        [HttpPost("Receive-cart")]
        public async Task<IActionResult> GetLocalCartOrder([FromBody] localCartOrderCollection localCartOrderCollection)
        {
            try
            {
                if (localCartOrderCollection == null)
                {
                    return BadRequest("El carrito enviado está vacío o no es válido.");
                }

                ICollection<OrderDetail> orderDetails = new List<OrderDetail>();
                

                foreach (var cartDetail in localCartOrderCollection.Details)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductID = cartDetail.ProductId,
                        Amount = cartDetail.Amount,
                        TemporaryOrderID = 150,
                        Product = cartDetail.Product
                    };
                    orderDetails.Add(orderDetail);
                }

                // Si se está utilizando un repositorio, por ejemplo:
                await _temporaryOrderRepository.AddTemporaryOrderAsync(orderDetails);

                return Ok(new
                {
                    message = "Carrito procesado correctamente",
                    itemsProcessed = localCartOrderCollection.Details.Count
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error procesando la solicitud: {ex.Message}");
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }

        }
    }
}
