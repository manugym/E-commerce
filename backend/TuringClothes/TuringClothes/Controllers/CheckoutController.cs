
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Repository;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly Settings _settings;
        private readonly TemporaryOrderRepository _temporaryOrderRepository;

        public CheckoutController(IOptions<Settings> options, TemporaryOrderRepository temporaryOrderRepository)
        {
            _settings = options.Value;
            _temporaryOrderRepository = temporaryOrderRepository;
        }

        /*[HttpGet("products")]
        public IEnumerable<ProductDto[]> GetAllProducts()
        {
            return GetProducts();
        }
        */

        [HttpGet("GetAllProductsDto")]
        public async Task<ProductOrderDto[]> GetAllProducts(long temporaryOrderId)
        {
            return await GetProducts(temporaryOrderId);
        }

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout(long temporaryOrderId)
        {
            ProductOrderDto[] products = await GetAllProducts(temporaryOrderId);
            List<SessionLineItemOptions> lineItems = new List<SessionLineItemOptions>();
            foreach (var product in products)
            {
                lineItems.Add(new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "eur",
                        UnitAmount = (long)(product.Price),
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Images = new List<string> { product.Image }
                        }
                    },
                    Quantity = product.Amount,
                });
            }
            SessionCreateOptions options = new SessionCreateOptions
            {
                UiMode = "embedded",
                Mode = "payment",
                PaymentMethodTypes = ["card"],
                LineItems = lineItems,
                CustomerEmail = "correo_cliente@correo.es",
                RedirectOnCompletion = "never"
            };

            SessionService service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new
            {
                clientSecret = session.ClientSecret
            });
        }

        [HttpGet("status/{sessionId}")]
        public async Task<ActionResult> SessionStatus(string sessionId)
        {
            SessionService sessionService = new SessionService();
            Session session = await sessionService.GetAsync(sessionId);

            return Ok(new { status = session.Status, customerEmail = session.CustomerEmail });
        }

        private async Task<ProductOrderDto[]> GetProducts(long temporaryOrderId)
        {
            TemporaryOrder temporaryOrder = await _temporaryOrderRepository.GetTemporaryOrder(temporaryOrderId);
            List<ProductOrderDto> productList = new List<ProductOrderDto>();
            foreach (var orderDetail in temporaryOrder.Details)
            {
                productList.Add(new ProductOrderDto
                {
                    Name = orderDetail.Product.Name,
                    Description = orderDetail.Product.Description,
                    Price = orderDetail.Product.Price,
                    Amount = orderDetail.Amount,
                    Image = orderDetail.Product.Image
                });

            }
            return productList.ToArray();
        }
    }
}
