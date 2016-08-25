using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MuhendisAsci.Web.Infrastructure
{
    public class MailHelper
    {
        public static bool SendMail(string body, string to, string subject, bool isHtml = true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }


        public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {
            bool result = false;

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(ConfigHelper.MailUid);

                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var smtp = new SmtpClient(ConfigHelper.MailHost, int.Parse(ConfigHelper.MailPort)))
                {
                    smtp.EnableSsl = false;
                    smtp.Credentials = new NetworkCredential(ConfigHelper.MailUid, ConfigHelper.MailPass);

                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception)
            {

            }

            return result;
        }
    }
}
