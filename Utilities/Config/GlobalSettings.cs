using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Factory;

namespace Utilities
{
   public class GlobalSettings
    {
        public static string SECTIONNAME = "全局设置";
        public static string LOOP_TIMES = "失败号循环次数";

        private static GlobalSettings instance = null;
        public int loopTimes = 3;
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
        }
        public void Save()
        {
            ConfigFactory.getInstance().WriteInteger(SECTIONNAME, LOOP_TIMES, loopTimes);
        }
    }
}
