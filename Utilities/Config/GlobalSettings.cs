using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Factory;
using System.Drawing;

namespace Utilities
{
   public class GlobalSettings
    {
        public static string SECTIONNAME = "全局设置";
        public static string LOOP_TIMES = "失败号循环次数";
        public static string IMAGE_LOOP_TIMES = "验证码失败时循环次数";
        public static string SMS_LOOP_TIMES = "短信接收失败时循环次数";
        public static string VPN_LOGIN = "VPN登录坐标";
        public static string VPN_DISCNN = "VPN断开坐标";
        public static string TASK_TIMESOUT = "任务超时时间";
        public static string DAIL_TIMESOUT = "网速超时时间";
        public static string DAIL_SPEED = "网速大于(M/s)";

        public static string IP_ADDRESS = "拨号IP段";
        public static string IMAGE_PORJ_CODE_2 = "图片验证码编号2";
        private static GlobalSettings instance = null;
        public int loopTimes = 3;
        public int ImageLoopTimes = 3;
        public int smsLoopTimes = 10;
        public string adslGap = "B";
        public int imageProjectType = 1;

        // 任务的超时时间
        public int taskTimesout = 10;
        public int dailTimesout = 5;
        public double dailSpeed = 0.3;
        public Point vpn_login = Point.Empty;
        public Point vpn_discnn = Point.Empty;

        public static GlobalSettings getInstance()
        {
            if (instance == null)
            {
                instance = new GlobalSettings();
                instance.Parse();
            }
            return instance;
        }

        private void Parse()
        {
            loopTimes = ConfigFactory.getInstance().ReadInteger(SECTIONNAME, LOOP_TIMES, 3);
            adslGap = ConfigFactory.getInstance().ReadString(SECTIONNAME, IP_ADDRESS, "B");
            imageProjectType = ConfigFactory.getInstance().ReadInteger(ImgCheckAccount.SectionName, IMAGE_PORJ_CODE_2,4001);
            ImageLoopTimes = ConfigFactory.getInstance().ReadInteger(SECTIONNAME, IMAGE_LOOP_TIMES, 3);
            smsLoopTimes = ConfigFactory.getInstance().ReadInteger(SECTIONNAME, SMS_LOOP_TIMES, 10);
            taskTimesout = ConfigFactory.getInstance().ReadInteger(SECTIONNAME, TASK_TIMESOUT, 10);
            dailTimesout = ConfigFactory.getInstance().ReadInteger(SECTIONNAME, DAIL_TIMESOUT, 5);
            dailSpeed = Convert.ToDouble(ConfigFactory.getInstance().ReadString(SECTIONNAME, DAIL_SPEED, "0.3"));

            // 解析登录左边
            String vpnStr = ConfigFactory.getInstance().ReadString(SECTIONNAME, VPN_LOGIN, "0|0");
            String[] coords = vpnStr.Split(new char[] { '|' });
            if (coords.Length == 2)
            {
                int x = Convert.ToInt32(coords[0]);
                int y = Convert.ToInt32(coords[1]);
                vpn_login = new Point(x, y);
            }

            // 解析断开坐标
             vpnStr = ConfigFactory.getInstance().ReadString(SECTIONNAME, VPN_DISCNN, "0|0");
             coords = vpnStr.Split(new char[] { '|' });
             if (coords.Length == 2)
             {
                int x = Convert.ToInt32(coords[0]);
                int y = Convert.ToInt32(coords[1]);
                vpn_discnn = new Point(x, y);
             }

        }
        public void Save()
        {
            ConfigFactory.getInstance().WriteInteger(SECTIONNAME, LOOP_TIMES, loopTimes);
            ConfigFactory.getInstance().WriteString(SECTIONNAME, IP_ADDRESS, adslGap);
            ConfigFactory.getInstance().WriteInteger(SECTIONNAME, IMAGE_LOOP_TIMES, ImageLoopTimes);
            ConfigFactory.getInstance().WriteInteger(ImgCheckAccount.SectionName, IMAGE_PORJ_CODE_2, imageProjectType);
            ConfigFactory.getInstance().WriteInteger(SECTIONNAME, SMS_LOOP_TIMES, smsLoopTimes);
            ConfigFactory.getInstance().WriteInteger(SECTIONNAME, TASK_TIMESOUT, taskTimesout);
            ConfigFactory.getInstance().WriteInteger(SECTIONNAME, DAIL_TIMESOUT, dailTimesout);
            ConfigFactory.getInstance().WriteString(SECTIONNAME, DAIL_SPEED, dailSpeed.ToString());

            ConfigFactory.getInstance().WriteString(SECTIONNAME, VPN_LOGIN, string.Format("{0}|{1}", vpn_login.X, vpn_login.Y));
            ConfigFactory.getInstance().WriteString(SECTIONNAME, VPN_DISCNN, string.Format("{0}|{1}", vpn_discnn.X, vpn_discnn.Y));

        }
    }
}
