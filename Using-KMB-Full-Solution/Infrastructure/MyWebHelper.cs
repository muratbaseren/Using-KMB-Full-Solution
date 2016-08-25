using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MuhendisAsci.Web.Infrastructure
{
    public class EmailHelper
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Host { get; private set; }
        public int Port { get; private set; }


        public EmailHelper()
        {
            // Config'den okunur değerler..
            Username = ConfigurationManager.AppSettings["EmailUsername"];
            Password = ConfigurationManager.AppSettings["EmailPassword"];
            Host = ConfigurationManager.AppSettings["EmailHost"];
            Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
        }

        public EmailHelper(string username, string password, string host, int port)
        {
            Username = username;
            Password = password;
            Host = host;
            Port = port;
        }

        public void SendMail(string from, string to, string subject, string body)
        {
            // Eposta gönderme yetki ayarları..
            NetworkCredential kimlik = new NetworkCredential();
            kimlik.UserName = Username;
            kimlik.Password = Password;

            // Eposta sunucusu ayarları..
            SmtpClient client = new SmtpClient();
            client.Host = Host;
            client.Port = Port;

            client.Credentials = kimlik;    // Mail gönderme yetkisi sunucuya verilir.
            client.EnableSsl = true;        // Güvenlik açılır.

            // Gönderilecek mesaj..
            MailMessage posta = new MailMessage();
            posta.IsBodyHtml = true;        // Zengin içerikli mail gönderme için açılır.
            posta.Subject = subject;
            posta.Body = body;

            try
            {
                posta.From = new MailAddress(from);
                posta.To.Add(new MailAddress(to));

                client.Send(posta);
            }
            catch (Exception ex)
            {

            }
        }
    }
}