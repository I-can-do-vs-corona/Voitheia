using ActiveCruzer.Models.DTO.Request;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using MimeKit.Text;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ActiveCruzer.BLL
{
    public class EmailSenderBll : IEmailSenderBll
    {
        EmailConfiguration _emailconfiguration;
        private IHostingEnvironment _env;
        private string confirmationTemplate;
        private string passwordResetTemplate;
        private string deleteUserTemplate;
        public EmailSenderBll(IConfiguration configuration, IHostingEnvironment env)
        {
            _emailconfiguration = new EmailConfiguration();
            _emailconfiguration.MailServer = "smtp.strato.de";
            _emailconfiguration.MailPort = 587;
            _emailconfiguration.SenderName = "no-reply: Voitheia";
            _emailconfiguration.Sender = "info@voitheia.org";
            _emailconfiguration.Password = configuration["EmailPasswort"];
            _env = env;
            confirmationTemplate = _env.ContentRootPath
                + Path.DirectorySeparatorChar.ToString()
                + "BLL"
                + Path.DirectorySeparatorChar.ToString()
                + "EMAIL"
                + Path.DirectorySeparatorChar.ToString()
                + "TEMPLATES"
                + Path.DirectorySeparatorChar.ToString()
                + "Template-EmailConfirm"
                + Path.DirectorySeparatorChar.ToString()
                + "index.html";
            passwordResetTemplate = _env.ContentRootPath
                + Path.DirectorySeparatorChar.ToString()
                + "BLL"
                + Path.DirectorySeparatorChar.ToString()
                + "EMAIL"
                + Path.DirectorySeparatorChar.ToString()
                + "TEMPLATES"
                + Path.DirectorySeparatorChar.ToString()
                + "Template-PasswordReset"
                + Path.DirectorySeparatorChar.ToString()
                + "index.html";
            deleteUserTemplate = _env.ContentRootPath
                + Path.DirectorySeparatorChar.ToString()
                + "BLL"
                + Path.DirectorySeparatorChar.ToString()
                + "EMAIL"
                + Path.DirectorySeparatorChar.ToString()
                + "TEMPLATES"
                + Path.DirectorySeparatorChar.ToString()
                + "Template-DeleteUser"
                + Path.DirectorySeparatorChar.ToString()
                + "index.html";
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
                string body = "";
                using (StreamReader SourceReader = System.IO.File.OpenText(confirmationTemplate))
                {
                    var builder = new StringBuilder();
                    builder.Append(SourceReader.ReadToEnd());
                    builder.Replace("{{LINK}}", mess);
                    builder.Replace("{{FIRSTNAME}}", firstname);
                    body = builder.ToString();
                }

                // message
                var message = new MailMessage()
                {
                    From = new MailAddress(_emailconfiguration.Sender, _emailconfiguration.SenderName),
                    Subject = "Voitheia E-Mail Bestätigung",
                    //Body = "Hallo und Willkommen zu Voitheia! "+ firstname + ",<br><br>Bitte bestätigen Sie Ihre E-Mail Adresse um die volle FUnktionalität von Voitheia zu geniesen.<br>Bitten klicken Sie hierzu auf den folgenden Link.<br>" + mess + "<br>Mit freundlichen Grüßen,<br><br> Voitheia Service Team",
                    Body = body,
                    IsBodyHtml = true
                };

                // add sender
                message.To.Add(new MailAddress(email));

                // Smtp client
                using(var client = new SmtpClient())
                {
                    client.Port = _emailconfiguration.MailPort;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = _emailconfiguration.MailServer;
                    client.EnableSsl = true;
                    client.Credentials = credentials;
                    client.Send(message);
                    Trace.WriteLine("Email sent!");
                };
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

                string body = "";
                using (StreamReader SourceReader = System.IO.File.OpenText(passwordResetTemplate))
                {
                    var builder = new StringBuilder();
                    builder.Append(SourceReader.ReadToEnd());
                    builder.Replace("{{LINK}}", mess);
                    builder.Replace("{{FIRSTNAME}}", firstname);
                    body = builder.ToString();
                }

                // message
                var message = new MailMessage()
                {
                    From = new MailAddress(_emailconfiguration.Sender, _emailconfiguration.SenderName),
                    Subject = "Voitheia Password Reset",
                    Body = body,
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
        public Task SendDeleteEmailAsync(string firstname, string email)
        {
            try
            {
                // build credentials
                var credentials = new NetworkCredential(_emailconfiguration.Sender, _emailconfiguration.Password);

                string body = "";
                using (StreamReader SourceReader = System.IO.File.OpenText(deleteUserTemplate))
                {
                    var builder = new StringBuilder();
                    builder.Append(SourceReader.ReadToEnd());
                    builder.Replace("{{FIRSTNAME}}", firstname);
                    body = builder.ToString();
                }

                // message
                var message = new MailMessage()
                {
                    From = new MailAddress(_emailconfiguration.Sender, _emailconfiguration.SenderName),
                    Subject = "Voitheia Information Userlöschung",
                    Body = body,
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
