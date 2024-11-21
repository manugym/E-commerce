using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MyDatabase _mydatabase;
        private readonly IEmailService _emailService;

        public MailController(MyDatabase dbContext, IEmailService emailService)
        {
            _mydatabase = dbContext;
            _emailService = emailService;
        }
        [HttpPost("send-confirmation")]
        public async Task<IActionResult> SendConfirmationEmail([FromBody] int mailId)
        {
            // Obtener el mail y sus datos relacionados
            var mail = await _mydatabase.Email
                .Include(m => m.User) // Incluir la relación con User
                .Include(m => m.TemporaryOrderDetail)
                    .ThenInclude(detail => detail.Product)
                .FirstOrDefaultAsync(m => m.Id == mailId);

            if (mail == null)
                return NotFound(new { Message = "Mail not found" });

            // Obtener detalles del usuario
            var buyer = mail.User;
            if (buyer == null)
                return NotFound(new { Message = "User not found" });

            var productDetails = mail.TemporaryOrderDetail;

            // Generar el cuerpo del correo
            string emailBody = GeneratePurchaseEmail(buyer.Name, productDetails);

            // Enviar el correo
            await _emailService.SendEmailAsync(buyer.Email, "Purchase Confirmation", emailBody);

            return Ok(new { Message = "Purchase confirmation email sent." });
        }

        private string GeneratePurchaseEmail(string buyerName, TemporaryOrderDetail detail)
        {
            // Detalles del producto
            string productInfo = $@"
            <li>{detail.Product.Name} - {detail.Amount} x {detail.Product.Price:C}</li>";

            return $@"
            <h1>Thank you for your purchase, {buyerName}!</h1>
            <p>Here are the details of your purchase:</p>
            <ul>
                {productInfo}
            </ul>
            <p><strong>Total: {detail.Amount * detail.Product.Price:C}</strong></p>";
        }
    }
}
}
