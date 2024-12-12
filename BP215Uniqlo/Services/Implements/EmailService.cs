using BP215Uniqlo.Helpers;
using BP215Uniqlo.Services.Abstracts;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace BP215Uniqlo.Services.Implements
{
//    public class EmailServices : IEmailServices
//    {
       
//            readonly SmtpOptions _smtpOptions;
//            public EmailServices(IOptions<SmtpOptions> options)
//            {
//                _smtpOptions = options.Value;
//            }
//            public Task SendAsync()
//            {
//                var a = _smtpOptions;
//            SmtpClient smtp = new();
//            smtp.Host = _smtpOpt.Host;
//            smtp.Port = _smtpOpt.Port;
//            smtp.EnableSsl = true;
//            smtp.Credentials = new NetworkCredential(_smtpOpt.Username, _sm+tpOpt.Password);
//            MailAddress from = new MailAddress(_smtpOpt.Username, "CAVIDAN COMPANY");
//            MailAddress to = new("mematisirinov31@gmail.com");
//            MailMessage msg = new MailMessage(from, to);
//            msg.Subject = "Security alert!";
//            msg.Body = " ee ne dost e  memati";
//            smtp.Send(msg);
//            return Task.CompletedTask;
//            }

//    }
}
