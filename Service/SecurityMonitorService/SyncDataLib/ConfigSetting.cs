using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;


namespace SyncDataLib
{
    public class ConfigSetting
    {
        public static readonly ConfigSetting configSetting = new ConfigSetting();

        public string IsOpenLog { get; set; }

        public string IsParaseData { get; set; }

        public string RemoteFilePath { get; set; }

        public string RemoteFileName { get; set; }

        public string RemoteUserName { get; set; }

        public string RemoteIP { get; set; }

        public string RemotePassword { get; set; }

        public string Interval { get; set; }

        public string DataSource { get; set; }

        public string DBConnection { get; set; }

        public string DownLoadFileDir { get; set; }

        public string IsDummyData { get; set; }

        private ConfigSetting()
        {
           RemoteFilePath = ConfigurationManager.AppSettings["RemoteFilePath"];

           RemoteFileName = ConfigurationManager.AppSettings["RemoteFileName"];

           Interval = ConfigurationManager.AppSettings["Interval"];

           DBConnection = ConfigurationManager.AppSettings["DBConnection"];

           DownLoadFileDir = ConfigurationManager.AppSettings["DownLoadFileDir"];

           DataSource = ConfigurationManager.AppSettings["DataSource"];

           RemotePassword = ConfigurationManager.AppSettings["RemotePassword"];

           RemoteUserName = ConfigurationManager.AppSettings["RemoteUserName"];

           RemoteIP = ConfigurationManager.AppSettings["RemoteIP"];

           IsParaseData = ConfigurationManager.AppSettings["IsParaseData"];

           IsOpenLog = ConfigurationManager.AppSettings["IsOpenLog"];

           IsDummyData = ConfigurationManager.AppSettings["IsDummyData"];
        }

    }
}
