using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace TicketVerkoop.Util.Mail
{
    public class EmailSender
    {
        public interface IEmailSend
        {
            Task SendEmailAsync(string email, string subject, string message);
        }

        public class EmailSend : IEmailSend
        {
            private readonly EmailSettings _emailSettings;


            public EmailSend(IOptions<EmailSettings> emailsettings)
            {
                _emailSettings = emailsettings.Value;
            }

            public async Task SendEmailAsync(string email, string subject, string message)
            {
                var mail = new MailMessage();  //aanmaken van een mail-object

                mail.To.Add(new MailAddress(email));
                mail.From = new MailAddress("matthewdesramault@gmail.com");
                mail.Subject = subject;
                mail.Body = message;

                mail.IsBodyHtml = true;

                try
                {
                    using (var smtp = new SmtpClient(_emailSettings.MailServer))
                    {
                        smtp.Port = _emailSettings.MailPort;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);

                        await smtp.SendMailAsync(mail);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
