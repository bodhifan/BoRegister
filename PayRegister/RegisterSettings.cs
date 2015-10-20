using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRegister
{
    class RegisterSettings
    {
        private static RegisterSettings instance = null;
        public static RegisterSettings getInstance()
        {
            if (instance == null)
            {
                instance = new RegisterSettings();
            }
            return instance;
        }
        public static string WATTING_TIMES_FOR_DAIL = "拨号间隔时间";
        public int waittingTimes = 5;
    }
}
