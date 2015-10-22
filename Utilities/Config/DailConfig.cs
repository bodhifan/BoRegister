using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Factory;
using DotRas;
using System.Collections.ObjectModel;

namespace Utilities
{
    public class DailConfig
    {
        public string SECTION_NAME_KEY = "宽带设置";
        public string DAIL_NAME = "宽带名称";
        public string DAIL_WAITTING_TIME = "断网等待时间";
        public string NAME = "账号";
        public string PWD = "密码";
        public string GAP_DAIL = "间隔(几次)拨号";
        public string dailName;
        public int waittingTime;
        public string userName;
        public string password;
        public int dailGap;
        private static DailConfig instance = null;

        private static ADSLConnetion sinAsdl = null;

        public static DailConfig getInstance()
        {
            if (instance == null)
            {
                instance = new DailConfig();
                instance.Parse();

                sinAsdl = new ADSLConnetion();
                sinAsdl.user = instance.userName;
                sinAsdl.password = instance.password;
            }
            return instance;
        }
        public void Save()
        {
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, DAIL_NAME, dailName);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, DAIL_WAITTING_TIME, waittingTime.ToString());
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, NAME, userName);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, PWD, password);
            ConfigFactory.getInstance().WriteInteger(SECTION_NAME_KEY, GAP_DAIL, dailGap);
        }
         public void Parse()
         {
             dailName = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, DAIL_NAME, SinASDL.GetASDLNames()[0]);
             waittingTime = ConfigFactory.getInstance().ReadInteger(SECTION_NAME_KEY, DAIL_WAITTING_TIME, 3);
             userName = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, NAME, "");
             password = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, PWD, "");
             dailGap = ConfigFactory.getInstance().ReadInteger(SECTION_NAME_KEY, GAP_DAIL, 5);
         }

        public void DisConnect()
         {
             ReadOnlyCollection<RasConnection> rasConns = RasConnection.GetActiveConnections();
             foreach (RasConnection cnn in rasConns)
             {
                 cnn.HangUp();
             }
         }
        public int Connect()
        {
           return sinAsdl.Connect(instance.dailName);
        }
    }
}
