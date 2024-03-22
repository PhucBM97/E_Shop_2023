using Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Services.Interfaces;
using MailKit.Net.Smtp;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void SendEmail(EmailModel emailModel)
        {
            // nhận 1 đối tượng email -> MimeMessage
            var emailMessage = new MimeMessage();
            var from = _config["EmailSettings:From"]; // config trong appsetting.json
            emailMessage.From.Add(new MailboxAddress("Shop", from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To, emailModel.To)); // địa chỉ email gửi đến
            emailMessage.Subject = emailModel.Subject; // tiêu đề
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content) // nội dung ( html string )
            };

            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_config["EmailSettings:SmtpServer"], 465, true);
                    client.Authenticate(_config["EmailSettings:From"], _config["EmailSettings:Password"]);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
