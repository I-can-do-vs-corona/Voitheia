using ActiveCruzer.Models.DTO.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ActiveCruzer.BLL
{
    public class EmailSenderBll : IEmailSenderBll
    {
        EmailConfiguration _emailconfiguration;
        public EmailSenderBll(IConfiguration configuration)
        {
            _emailconfiguration = new EmailConfiguration();
            _emailconfiguration.MailServer = "smtp.strato.de";
            _emailconfiguration.MailPort = 587;
            _emailconfiguration.SenderName = "no-reply: Voitheia";
            _emailconfiguration.Sender = "info@voitheia.org";
            _emailconfiguration.Password = configuration["EmailPasswort"];
        }

        public void Dispose()
        {

        }

        public Task SendEmailConfirmationAsync(string firstname, string email, string mess)
        {
            try
            {
                // build credentials
                var credentials = new NetworkCredential(_emailconfiguration.Sender, _emailconfiguration.Password);

                // message
                var message = new MailMessage()
                {
                    From = new MailAddress(_emailconfiguration.Sender, _emailconfiguration.SenderName),
                    Subject = "Voitheia E-Mail Bestätigung",
                    Body = "Hallo und Willkommen zu Voitheia! "+ firstname + ",<br><br>Bitte bestätigen Sie Ihre E-Mail Adresse um die volle FUnktionalität von Voitheia zu geniesen.<br>Bitten klicken Sie hierzu auf den folgenden Link.<br>" + mess + "<br>Mit freundlichen Grüßen,<br><br> Voitheia Service Team",
                    IsBodyHtml = true
                };

                // add sender
                message.To.Add(new MailAddress(email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = _emailconfiguration.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = _emailconfiguration.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(message);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

            return Task.CompletedTask;
        }
        public Task SendEmailPWTokenAsync(string firstname, string email, string mess)
        {
            try
            {
                // build credentials
                var credentials = new NetworkCredential(_emailconfiguration.Sender, _emailconfiguration.Password);

                // message
                var message = new MailMessage()
                {
                    From = new MailAddress(_emailconfiguration.Sender, _emailconfiguration.SenderName),
                    Subject = "Voitheia Password Reset",
                    Body = mess,
                    IsBodyHtml = true
                };

                // add sender
                message.To.Add(new MailAddress(email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = _emailconfiguration.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = _emailconfiguration.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(message);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

            return Task.CompletedTask;
        }

    }

    public class EmailConfiguration
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }

    }
}
