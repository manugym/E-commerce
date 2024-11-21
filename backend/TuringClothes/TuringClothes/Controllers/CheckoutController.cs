using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using TuringClothes.Model;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly Settings _settings;

        public CheckoutController(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        [HttpGet("products")]
        public IEnumerable<ProductDto> GetAllProducts()
        {
            return GetProducts();
        }

        [HttpGet("hosted")]
        public async Task<ActionResult> HostedCheckout()
        {
            ProductDto product = GetProducts()[0];

            SessionCreateOptions options = new SessionCreateOptions
            {
                UiMode = "hosted",
                Mode = "payment",
                PaymentMethodTypes = ["card"],
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "eur",
                        UnitAmount = (long)(product.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Images = [product.Image]
                        }
                    },
                    Quantity = 1,
                },
            },
                CustomerEmail = "correo_cliente@correo.es",
                SuccessUrl = _settings.ClientBaseUrl + "/checkout?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = _settings.ClientBaseUrl + "/checkout"
            };

            SessionService service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { sessionUrl = session.Url });
        }

        [HttpGet("embedded")]
        public async Task<ActionResult> EmbededCheckout()
        {
            ProductDto product = GetProducts()[0];

            SessionCreateOptions options = new SessionCreateOptions
            {
                UiMode = "embedded",
                Mode = "payment",
                PaymentMethodTypes = ["card"],
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "eur",
                        UnitAmount = (long)(product.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = product.Name,
                            Description = product.Description,
                            Images = [product.Image]
                        }
                    },
                    Quantity = 1,
                },
            },
                CustomerEmail = "correo_cliente@correo.es",
                ReturnUrl = _settings.ClientBaseUrl + "/checkout?session_id={CHECKOUT_SESSION_ID}",
            };

            SessionService service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { clientSecret = session.ClientSecret });
        }

        [HttpGet("status/{sessionId}")]
        public async Task<ActionResult> SessionStatus(string sessionId)
        {
            SessionService sessionService = new SessionService();
            Session session = await sessionService.GetAsync(sessionId);

            return Ok(new { status = session.Status, customerEmail = session.CustomerEmail });
        }

        private ProductDto[] GetProducts()
        {
            return [
                new ProductDto
            {
                Name = "Rana epiléptica",
                Description = "¡Presentamos la Rana Epiléptica! El DJ del estanque que transforma cualquier charca en una fiesta con sus saltos y croac descontrolados. ¡Lleva la diversión anfibia a otro nivel!",
                Price = 100,
                Image = Request.GetAbsoluteUrl("")
            }
            ];
        }
    }
}
