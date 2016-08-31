using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Using_KMB_Full_Solution.Infrastructure;

namespace Using_KMB_Full_Solution.Infrastructure
{
    public class MyConfigHelper
    {
        public static string DevCompanyName = ConfigurationManager.AppSettings[AppSettingKeys.DevCompanyName];
        public static string DevEMailAddress = ConfigurationManager.AppSettings[AppSettingKeys.DevEMailAddress];
        public static string DevWebPageUri = ConfigurationManager.AppSettings[AppSettingKeys.DevWebPageUri];
        public static string ErrorMailSubject = ConfigurationManager.AppSettings[AppSettingKeys.ErrorMailSubject];
        public static string MailHost = ConfigurationManager.AppSettings[AppSettingKeys.MailHost];
        public static string MailPort = ConfigurationManager.AppSettings[AppSettingKeys.MailPort];
        public static string MailUid = ConfigurationManager.AppSettings[AppSettingKeys.MailUid];
        public static string MailPass = ConfigurationManager.AppSettings[AppSettingKeys.MailPass];
        public static string NotifyMailAddress = ConfigurationManager.AppSettings[AppSettingKeys.NotifyMailAddress];
    }
}
