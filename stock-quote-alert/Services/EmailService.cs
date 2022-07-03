using stock_quote_alert.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Services
{
    class EmailService
    {
        private string SMTPHost;
        private int SMTPPort;
        private string SMTPUsername;
        private string SMTPPassword;
        private string SMTPFromAddress;
        public EmailService()
        {
            var configuration = SecretConfiguration.GetInstance();
            SMTPHost = configuration.GetSection("smtpClientHost").Value;
            SMTPPort = Convert.ToInt32(configuration.GetSection("smtpClientPort").Value);
            SMTPUsername = configuration.GetSection("smtpClientUsername").Value;
            SMTPPassword = configuration.GetSection("smtpClientPassword").Value;
            SMTPFromAddress = configuration.GetSection("smtpClientFromAddress").Value;
        }
        public void SendMail(string to, string subject, string content, string? markupContent)
        {
            using var message = new MailMessage();
            message.From = new MailAddress(SMTPFromAddress);
            message.To.Add(
                new MailAddress(
                    to
                )
            );
            message.Subject = subject;
            if(markupContent != null)
                message.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(
                        markupContent, null, MediaTypeNames.Text.Html
                    )
                );
            message.AlternateViews.Add(
                AlternateView.CreateAlternateViewFromString(
                    content, null, MediaTypeNames.Text.Plain
                )
            );
            try
            {
                using var client = new SmtpClient(host: SMTPHost, port: SMTPPort)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        userName: SMTPUsername,
                        password: SMTPPassword
                    )
                };

                client.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
