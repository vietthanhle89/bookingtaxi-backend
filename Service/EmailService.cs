using bookingtaxi_backend.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace bookingtaxi_backend.Service
{
    public class EmailService
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        
        public EmailService(IOptions<EmailSettings> settings)
        {      
            _emailSettings = settings;
        }

        private MailMessage ComposeEmail(String addresses, String subject, String body) {
            MailMessage mail = new MailMessage();

            mail.To.Add(addresses);
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = false;
            mail.From = new MailAddress(_emailSettings.Value.Sender, "Booking Taxi", Encoding.UTF8);

            return mail;
        }



        private void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(_emailSettings.Value.Sender, _emailSettings.Value.Password);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(mail);
        }

        public void SendWelcomeEmail(String addresses) {
            SendEmail(ComposeEmail(addresses, "Welcome to Booking Taxi", "Hello"));
        }


        
    }
}
