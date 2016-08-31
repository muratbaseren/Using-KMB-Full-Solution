using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Using_KMB_Full_Solution.Infrastructure
{
    public class MySessionNames
    {
        public const string login = "kmbsyslogin";
    }

    public class MyMagicStrings
    {
        public const string noname = "noname";
    }

    public class MyTempDataKeys
    {
        public const string lasterror = "LastError";
    }

    public class AppSettingKeys
    {
        /* Copy and paste to appsettings section into web.config
        
        <add key="UploadImagePathName" value="/uploads" />
        <add key="DevCompanyName" value="Company Name" />
        <add key="DevWebPageUri" value="Developer Web Page Url" />
        <add key="DevEMailAddress" value="Developer mail address" />
        <add key="ErrorMailSubject" value="Error log mail subject" />
        <add key="MailHost" value="Mail Host Address" />
        <add key="MailPort" value="Mail Host Port Number" />
        <add key="MailUid" value="Mail Username(email address)" />
        <add key="MailPass" value="Mail password" />
        <add key="NotifyMailAddress" value="Notify Customer Mail Address" /> 

        */
        public const string DevCompanyName = "DevCompanyName";
        public const string DevEMailAddress = "DevEMailAddress";
        public const string DevWebPageUri = "DevWebPageUri";
        public const string ErrorMailSubject = "ErrorMailSubject";
        public const string MailHost = "MailHost";
        public const string MailPort = "MailPort";
        public const string MailUid = "MailUid";
        public const string MailPass = "MailPass";
        public const string NotifyMailAddress = "NotifyMailAddress";
    }
}