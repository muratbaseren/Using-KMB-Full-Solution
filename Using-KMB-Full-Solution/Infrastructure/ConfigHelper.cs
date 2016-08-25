using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuhendisAsci.Web.Infrastructure
{
    public class ConfigHelper
    {
        public static string DevCompanyName = ConfigurationManager.AppSettings["DevCompanyName"];
        public static string DevEMailAddress = ConfigurationManager.AppSettings["DevEMailAddress"];
        public static string DevWebPageUri = ConfigurationManager.AppSettings["DevWebPageUri"];
        public static string ErrorMailSubject = ConfigurationManager.AppSettings["ErrorMailSubject"];
        public static string MailHost = ConfigurationManager.AppSettings["MailHost"];
        public static string MailPort = ConfigurationManager.AppSettings["MailPort"];
        public static string MailUid = ConfigurationManager.AppSettings["MailUid"];
        public static string MailPass = ConfigurationManager.AppSettings["MailPass"];
        public static string NotifyMailAddress = ConfigurationManager.AppSettings["NotifyMailAddress"];
    }
}
