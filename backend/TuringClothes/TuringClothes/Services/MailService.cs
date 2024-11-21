using System.Net;
using System.Net.Mail;
using TuringClothes.Database;

namespace TuringClothes.Services
{
    public interface IMail
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
    public class MailService : IMail
    {

        private readonly MyDatabase _database;

        public MailService(MyDatabase database) 
        {
            _database = database;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient(_database["Email:SmtpServer"])
            {
                Port = int.Parse(_database["Email:Port"]),
                Credentials = new NetworkCredential(_database["Email:Username"], _database["Email:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_database["Email:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);    
        }

        public string GeneratePurchaseEmail(PurchaseDetails details)
        {
            var productList = string.Join("", details.Products.Select(p =>
                $"<li>{p.Name} - {p.Quantity} x {p.UnitPrice:C}</li>"));

            return $@"
            <h1>Gracias por su compra, {details.BuyerName}!</h1>
            <p>Aquí está el detalle del pedido:</p>
            <ul>
                {productList}
            </ul>
            <p><strong>Total: {details.TotalPrice:C}</strong></p>";
        }
        public async Task SendPurchaseConfirmationEmail(PurchaseDetails details)
        {
            // Generar el cuerpo del correo
            string emailBody = GeneratePurchaseEmail(details);

            // Enviar el correo
            await SendEmailAsync(details.BuyerEmail, "Purchase Confirmation", emailBody);
        }
    }

}
